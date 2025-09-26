using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CopilotAgentCodeReviewTest
{
    internal class Program
    {
        // Safe properties for BMI calculation - these would typically come from user input or database
        public static float Height { get; set; } = 170.0f; // Default height in cm
        public static float Weight { get; set; } = 70.0f;  // Default weight in kg

        static void Main(string[] args)
        {
        }

        public static float CalculateBMI()
        {
            float result = 0;
            
            // Prevent division by zero and invalid inputs
            if (Height <= 0 || Weight <= 0)
            {
                throw new ArgumentException("Height and Weight must be positive values");
            }
            
            float heightInMeters = Height / 100.0f;
            result = Weight / (heightInMeters * heightInMeters);
            
            return result;
        }

        public static void Save(string userInputFileName)
        {
            // Get password from environment variable instead of hardcoding
            string password = Environment.GetEnvironmentVariable("MYAPP_PASSWORD");
            if (string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException("Password environment variable 'MYAPP_PASSWORD' is not set.");
            }
            
            // Use cryptographically secure random number generator
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[4];
                rng.GetBytes(randomBytes);
                int secureToken = BitConverter.ToInt32(randomBytes, 0);
                Console.WriteLine(Math.Abs(secureToken)); // Use absolute value to avoid negative display
            }

            // Prevent path traversal by validating and using Path.Combine
            if (string.IsNullOrWhiteSpace(userInputFileName))
            {
                throw new ArgumentException("Filename cannot be null or empty");
            }
            
            // Remove any path separators to prevent directory traversal
            string safeFileName = Path.GetFileName(userInputFileName);
            if (string.IsNullOrEmpty(safeFileName))
            {
                throw new ArgumentException("Invalid filename provided");
            }
            
            string baseDirectory = @"D:\some\directory";
            string filePath = Path.Combine(baseDirectory, safeFileName);
            
            // Additional check to ensure the resolved path is within the intended directory
            string fullBasePath = Path.GetFullPath(baseDirectory);
            string fullFilePath = Path.GetFullPath(filePath);
            
            if (!fullFilePath.StartsWith(fullBasePath, StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("Access to the specified path is denied");
            }
            
            // Only read the file if it exists
            if (File.Exists(filePath))
            {
                File.ReadAllText(filePath);
            }
        }

        public static DataTable GetUserData(string userInput, SqlConnection connection)
        {
            DataTable dt = new DataTable();
            
            // Use parameterized query to prevent SQL injection
            string query = "select * from any.USERS where user_name = @userName";
            
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@userName", userInput);
                
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            
            return dt;
        }
    }
}
