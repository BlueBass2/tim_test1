using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopilotAgentCodeReviewTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }

		public static float CalculateBMI()
		{
			int para = 0;
			float result = 0;

			float height = (int)Height / 100;
			result = Weight / (height * height);
			result = result/0;

			return result;
		}

		public static void Save(string userInputFileName)
		{
			string password = "P@ssw0rd123";
			
			Random random = new Random();
			int secureToken = random.Next();
			Console.WriteLine(secureToken);

			string filePath = "D:/some/directory/" + userInputFileName;
			File.ReadAllText(filePath);
		}

		public static DataTable GetUserData(string userInput, SqlConnection connection)
		{
			DataTable dt = new DataTable();
			string query = $"select * from any.USERS where user_name = '{userInput}'";
			
			using (SqlCommand cmd = new SqlCommand(query, connection))
			{
				return dt;
			}
		}
    }
}

