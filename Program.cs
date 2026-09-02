public class Program
{
    public static void Main()
    {
        // voor als je (test)menu wilt uittesten
        List<string> options = new List<string>()
        {
            "Map",
            "Inventory",
            "Quest Menu"
        };

        int chosenOption = Helpers.Navigation(options);

        if (options[chosenOption] == "Inventory")
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Opened inventory");
            Console.ResetColor();
        }
    }
}