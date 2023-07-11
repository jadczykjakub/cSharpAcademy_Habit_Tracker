using Microsoft.Data.Sqlite;
using System.Globalization;
using static Program;

namespace cSharpAcademy_Habit_Tracker
{
    internal static class CrudEngine
    {
        internal static void Insert()
        {
            Console.WriteLine("Insert");
            Console.WriteLine("Please give me date. Format: dd-mm-yy");

            string date = Helpers.ValidationDate(Console.ReadLine());
            Console.WriteLine("Please give me Quatnity");

            int quantity = Convert.ToInt32(Helpers.ValidationNumber(Console.ReadLine()));

            using (var connection = new SqliteConnection(Program.connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"INSERT INTO drinking_water(date, quantity) VALUES('{date}', {quantity})";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        internal static void GetAllRecords()
        {
            Console.Clear();
            Console.WriteLine("There are all data");
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"SELECT * FROM drinking_water";

                SqliteDataReader reader = tableCmd.ExecuteReader();

                List<Model.DrinkingWater> tableData = new List<Model.DrinkingWater>();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                            new Model.DrinkingWater
                            {
                                Id = reader.GetInt32(0),
                                Date = DateTime.ParseExact(reader.GetString(1), "MM-dd-yy", new CultureInfo("en-US")),
                                Quantity = reader.GetInt32(2)
                            });
                    }
                }
                else
                {
                    Console.WriteLine("No rows was found");
                }

                connection.Close();
                Console.WriteLine("------------------------------");
                foreach (var dw in tableData)
                {
                    Console.WriteLine($"{dw.Id} - {dw.Date.ToString("dd-MM-yyyy")} Quantity: {dw.Quantity}");
                }
                Console.WriteLine("------------------------------");
            }
        }


        internal static void Delete()
        {
            Console.WriteLine("Please give Id row you would like to delete");
            int idRowToDelete = Convert.ToInt32(Helpers.ValidationNumber(Console.ReadLine()));

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"DELETE FROM drinking_water WHERE Id = {idRowToDelete}";

                int rowCount = tableCmd.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    Console.WriteLine($"Record with Id {idRowToDelete} doesn't exist");
                    Delete();
                }

                connection.Close();
            }
        }

        internal static void Update()
        {
            Console.WriteLine("Please give Id row you would like to update");
            int idRowToUpdate = Convert.ToInt32(Helpers.ValidationNumber(Console.ReadLine()));

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM drinking_water WHERE Id = {idRowToUpdate})";
                int checkQuery = Convert.ToInt32(tableCmd.ExecuteScalar());

                if (checkQuery == 0)
                {
                    Console.WriteLine($"Record with {idRowToUpdate} doesn't exist");
                    Update();
                }

                Console.WriteLine("Please give me date. Format: dd-mm-yy");
                string date = Helpers.ValidationDate(Console.ReadLine());

                Console.WriteLine("Please give me Quatnity");
                int quantity = Convert.ToInt32(Helpers.ValidationNumber(Console.ReadLine()));

                tableCmd.CommandText = $"UPDATE drinking_water SET Date = '{date}', Quantity = '{quantity}' WHERE Id = {idRowToUpdate}";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

        }
    }
}
