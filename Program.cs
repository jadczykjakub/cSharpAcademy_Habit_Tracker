using cSharpAcademy_Habit_Tracker;

class Program
{
    public static string connectionString = @"Data Source=habit-Tracker.db";
    static void Main(string[] args)
    {
        Helpers.CheckIfDbTableExist();
        Menu.ShowMenu();
    }
}


