using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MovieDapperProject.Models
{
    public class Context
    {
        public static string connectionString = 
            @"Server=DESKTOP-74G97I3\SQLEXPRESS;Database=MovieMvcDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public static void ExecuteReturn(string procName, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(procName, param, commandType: CommandType.StoredProcedure);
            }
        }

        public static IEnumerable<T> Listeleme<T>(string procName, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<T>(procName, param, commandType: CommandType.StoredProcedure).ToList();
            }

        }
    }
}

