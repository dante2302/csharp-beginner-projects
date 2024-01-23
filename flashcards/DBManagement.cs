using System.Configuration;
using DBClasses;
using System.Data.SqlClient;

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
        public static void Create(string Front, string Back, int Stack)
        {
            string commandText = $@"
                INSERT INTO Cards(Front, Back, Stack) 
                VALUES('{Front}', '{Back}', {Stack})";
            ExecNonQueryCmd(commandText);
        }

        public static void Edit(int cardId, string property, string editInfo)
        {
            string commandText = $@"
                UPDATE Cards 
                SET {property} = {editInfo}'
                WHERE Id = {cardId}";
            ExecNonQueryCmd(commandText);
        }

        public static void Delete(int cardId)
        {
            string commandText = $"DELETE FROM Cards WHERE Id = {cardId}";
            ExecNonQueryCmd(commandText);
        }

        public static List<Flashcard> GetNFromAStack(int stackId, int count=-1)
        {
            // if count is not specified, select all records.
            string commandText = $@"
                SELECT {(count == -1 ? "*" : count)} 
                FROM Cards WHERE Stack = {stackId}";

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
    }
}
