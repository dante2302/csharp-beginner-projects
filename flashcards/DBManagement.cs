using System.Configuration;
using System.Data.SqlClient;

namespace DBManagement
{
    class DataManager 
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["cstring"].ConnectionString;
        public static List<string> GetAllStackNames()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandText =
                    @"USE flashcards
                      SELECT * FROM Stacks";
                var cmd = new SqlCommand(commandText,connection);
                using(var reader = cmd.ExecuteReader())
                {
                    List<string> stackNameList = [];
                    while (reader.Read())
                    {
                        stackNameList.Add(
                            reader.GetString(reader.GetOrdinal("Topic"))); 
                    }
                    return stackNameList;
                }
            }
        }    
    }
}
