using System.Data.SqlClient;
using System.Configuration;

string connectionString = ConfigurationManager.ConnectionStrings["cstring"].ConnectionString;

using( var connection = new SqlConnection(connectionString))
{
 connection.Open();
    string statement = "INSERT INTO Stacks(Topic) VALUES('test')";
    using (var cmd = new SqlCommand(statement, connection))
    {
        cmd.ExecuteNonQuery();
    }
}
