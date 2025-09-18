using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }

        public static float CalculateBMI(float weight, float height)
        {
            float result = 0;

            if (height > 0)
            {
                float heightInMeters = height / 100;
                result = weight / (heightInMeters * heightInMeters);
            }

            return result;
        }

        public static void Save(string userInputFileName)
        {
            // Use environment variables or secure configuration for sensitive data
            // string password = Environment.GetEnvironmentVariable("APP_PASSWORD");
            
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] tokenBytes = new byte[4];
                rng.GetBytes(tokenBytes);
                int secureToken = BitConverter.ToInt32(tokenBytes, 0);
                Console.WriteLine(Math.Abs(secureToken)); // Use absolute value to avoid negative display
            }

            string filePath = Path.Combine("data", userInputFileName);
            // Ensure the directory exists and handle potential exceptions
            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                Console.WriteLine($"File content length: {fileContent.Length}");
            }
        }

        public static DataTable GetUserData(string userInput, SqlConnection connection)
        {
            DataTable dt = new DataTable();
            string query = "select * from any.USERS where user_name = @username";
            
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", userInput);
                
                // Example: Uncomment the following lines to fill the DataTable
                // using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                // {
                //     adapter.Fill(dt);
                // }
            }
            
            return dt;
        }
    }
}
