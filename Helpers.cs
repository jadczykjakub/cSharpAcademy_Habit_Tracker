using Microsoft.Data.Sqlite;

namespace cSharpAcademy_Habit_Tracker
{
    internal static class Helpers
    {
        internal static string? ValidationNumber(string userInput)
        {
            while (string.IsNullOrEmpty(userInput) || !Int32.TryParse(userInput, out _))
            {
                Console.WriteLine("Your answer needs to be an integer. Try again");
                userInput = Console.ReadLine();
            }
            return userInput;
        }
        internal static string ValidationDate(string userInput)
        {
            while (IsValidDateFormat(userInput) != true)
            {
                Console.WriteLine("Invalid date format. Please try again (Format: dd-mm-yy):");
                userInput = Console.ReadLine();
            }
            return userInput;
        }
        internal static bool IsValidDateFormat(string date)
        {
            // Regular expression pattern for dd-mm-yy format
            string pattern = @"^\d{2}-\d{2}-\d{2}$";
            return System.Text.RegularExpressions.Regex.IsMatch(date, pattern);
        }

        internal static void CheckIfDbTableExist()
        {
            using (var connection = new SqliteConnection(Program.connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS drinking_water (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Date TEXT,
                    Quantity INTEGER            
                    )";

                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
