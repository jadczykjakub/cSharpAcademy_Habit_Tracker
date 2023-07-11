namespace cSharpAcademy_Habit_Tracker
{
    internal static class Menu
    {
        internal static void ShowMenu()
        {
            Console.Clear();
            bool closeApp = false;

            while (closeApp == false)
            {
                Console.WriteLine("Main menu");
                Console.WriteLine("What would you like to do");
                Console.WriteLine("Type 0 to close application");
                Console.WriteLine("Type 1 to View all records");
                Console.WriteLine("Type 2 to Insert record");
                Console.WriteLine("Type 3 to Delete record");
                Console.WriteLine("Type 4 to Update record");

                string commandInput = Helpers.ValidationNumber(Console.ReadLine());

                switch (commandInput)
                {
                    case "0":
                        Console.WriteLine("Goodbye");
                        closeApp = true;
                        break;
                    case "1":
                        CrudEngine.GetAllRecords();
                        break;
                    case "2":
                        CrudEngine.Insert();
                        break;
                    case "3":
                        CrudEngine.Delete();
                        break;
                    case "4":
                        CrudEngine.Update();
                        break;
                    default:
                        Console.WriteLine("Invalid number, Please type number from 0 to 4");
                        break;
                }
            }
        }
    }
}
