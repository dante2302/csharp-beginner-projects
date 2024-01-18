using System.Configuration;
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
    }
    class StackRepo : DBRepo
    {
        public static void Create(string stackName)
        {
            ExecNonQueryCmd($"INSERT INTO Stacks(Topic) VALUES('{stackName}')");
        }
        public static List<string> GetAllStackNames()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandText =
                    "SELECT Topic FROM Stacks";
                var cmd = new SqlCommand(commandText, connection);
                using (var reader = cmd.ExecuteReader())
                {
                    List<string> stackNameList = [];
                    while (reader.Read())
                    {
                        stackNameList.Add(
                            reader.GetString(0));
                    }
                    return stackNameList;
                }
            }
        }

    }

    class DataManager 
    {
           }
}
