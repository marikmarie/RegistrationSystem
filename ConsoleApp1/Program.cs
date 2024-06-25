using System;
using System.Data.SqlClient;

namespace TestSqlConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=MARIAMTUKASINGU;Database=RegistartionDB;Integrated Security=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connection successful!");

                    // Test query
                    string query = "SELECT COUNT(*) FROM Registrations";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int count = (int)command.ExecuteScalar();
                        Console.WriteLine($"Number of registrations: {count}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}
