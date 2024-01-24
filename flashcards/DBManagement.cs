using System.Configuration;
using DBClasses;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace DBManagement
{
    internal class DBRepo 
    { 
        internal static readonly string connectionString = ConfigurationManager.ConnectionStrings["cstring"].ConnectionString;
        internal static int ExecNonQueryCmd(string command)
        {
            int affectedRows = 0;
            using (var connection  = new SqlConnection(connectionString))
            {
                connection.Open();

                string cmdText = command;

                using(var cmd = new SqlCommand(cmdText, connection))
                {
                    affectedRows = cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return affectedRows;
        }
        internal static void ExecReaderCmd(string commandText, Action<SqlDataReader> readerAction)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(commandText, connection);
                using (var reader = cmd.ExecuteReader())
                {
                    readerAction(reader);
                }
            }
        }

    }
    class StackRepo : DBRepo
    {
        public static void Create(string stackName)
        {
            ExecNonQueryCmd($"INSERT INTO Stacks(Topic) VALUES('{stackName}')");
        }

        public static void Edit(int stackId, string editInfo)
        {
            ExecNonQueryCmd(
                $@"
                UPDATE Stacks
                SET Topic = '{editInfo}'
                WHERE Id = {stackId}");
        }

        public static void Delete(int stackId)
        {
            ExecNonQueryCmd($"DELETE FROM Stacks WHERE Id = {stackId}");
        }

        public static List<Stack> GetAllStacks()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandText = "SELECT * FROM Stacks";

                var cmd = new SqlCommand(commandText, connection);
                using (var reader = cmd.ExecuteReader())
                {
                    List<Stack> stackList = [];
                    while (reader.Read())
                    {
                        stackList.Add(
                            new Stack 
                            { 
                                Id = reader.GetInt32(0), 
                                Topic = reader.GetString(1) 
                            });
                    }
                    return stackList;
                }
            }
        }
    }
    class FlashcardsRepo : DBRepo
    {
        public static bool Create(string Front, string Back, int Stack)
        {
            string commandText = $@"
                INSERT INTO Cards(Front, Back, Stack) 
                VALUES('{Front}', '{Back}', {Stack})";
            bool wasCreated = (ExecNonQueryCmd(commandText) == 1);
            return wasCreated;
        }

        public static bool Edit(int cardId, string property, string editInfo)
        {
            string commandText = $@"
                UPDATE Cards 
                SET {property} = '{editInfo}'
                WHERE Id = {cardId}";
            bool wasEdited = (ExecNonQueryCmd(commandText) == 1);
            return wasEdited;
        }

        public static bool Delete(int cardId)
        {
            string commandText = $"DELETE FROM Cards WHERE Id = {cardId}";
            bool wasDeleted = (ExecNonQueryCmd(commandText) == 1);
            return wasDeleted;
        }

        public static Flashcard GetById(int id)
        {
            string commandText = $"SELECT * FROM Cards WHERE Id = {id}";
            Flashcard card = new();
            card.Id = -1;
            ExecReaderCmd(commandText, reader =>
                {
                    if (reader.Read())
                    {
                        card.Id = reader.GetInt32(0);
                        card.Front = reader.GetString(1);
                        card.Back = reader.GetString(2);
                        card.StackId = reader.GetInt32(3);
                    }
                });
            return card;
        }

    public static List<Flashcard> GetNFromAStack(int stackId, int limit = 0, bool random = false)
        {
            // if count is not specified, select all records.
            string commandText = $@"
                SELECT 
                {(limit > 0 ? $"TOP {limit}" : "")}
                *
                FROM Cards WHERE Stack = {stackId}
                {(random ? "ORDER BY NEWID()" : "")}";

            List<Flashcard> cards = [];
            ExecReaderCmd(commandText, reader =>
                {
                    while (reader.Read())
                    {
                        cards.Add(
                            new Flashcard
                            {
                                Id = reader.GetInt32(0),
                                Front = reader.GetString(1),
                                Back = reader.GetString(2),
                                StackId = reader.GetInt32(3)
                            }
                            );
                    }
                });
            return cards;
        }

        public static List<Flashcard> GetFromASession(int sessionId, int topLimit = 0)
        {
            string commandText = "";

            if (topLimit != 0)
            {
                commandText = $@"
                    WITH Ranked AS(
                        SELECT 
                            Flashcard,
                        COUNT(*) AS occurence,
                        ROW_NUMBER() OVER (ORDER BY COUNT(*) DESC) AS Row_Num
                        FROM SessionQuestions
                        GROUP BY Flashcard
                    )

                    SELECT 
                        Flashcard,
                        occurence
                    FROM Ranked
                    WHERE Row_Num <= {topLimit}
                    )";
            }

            commandText = $@"
                SELECT Flashcard 
                FROM SessionQuestions 
                WHERE sessionId = {sessionId}";

            int[] topCardIds = [];

            ExecReaderCmd(commandText, reader =>
            {
                while (reader.Read())
                    topCardIds.Append(reader.GetInt32(0));
            });

            List<Flashcard> cards = [];

            foreach (int id in topCardIds)
            {
                var card = GetById(id);
                if(card.Id != -1)
                    cards.Add(card); 
            }

            return cards;
        }
    }
        class SessionRepo : DBRepo
    {
        public static bool Create(List<Flashcard> cardList, int stackId, int Points, int MaxPoints)
        {
            int newSessionId = -1;
            bool isGood = false;

            string cmdText = $@"
                INSERT INTO StudySessions (Points, MaxPoints, Stack) 
                OUTPUT INSERTED.ID VALUES ({Points},{MaxPoints},{stackId});";

            ExecReaderCmd(cmdText,reader =>
            {
                if(reader.Read())
                {
                    newSessionId = reader.GetInt32(0);
                }
            });
            if (newSessionId != -1)
            {
                foreach (Flashcard card in cardList)
                {

                    isGood = (ExecNonQueryCmd($@"
                                INSERT INTO SessionQuestions(Session, Flashcard)
                                VALUES({newSessionId}, {card.Id});") 
                              == 1); 
                }
            }
            return isGood;
        }

        public static List<StudySession> GetFromAStack(int stackId, int limit=0)
        {
            List<StudySession> sessionList = [];

            string cmdText = $"SELECT * FROM StudySessions WHERE StackId = {stackId}";
            ExecReaderCmd(cmdText, reader =>
            {
                while (reader.Read())
                {
                    sessionList.Add(
                        new StudySession
                        {
                            id = reader.GetInt32(0),
                            maxPoints = reader.GetInt32(1),
                            points = reader.GetInt32(2)
                        }
                        );
                }
            });
            return sessionList;
        }
    }
}
