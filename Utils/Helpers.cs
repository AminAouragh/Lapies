public static class Helpers
{
    public static int Navigation(List<string> MenuOptions)
    {
       int selectedIndex = 0;
       ConsoleKey key;

       do
        {
            Console.Clear();
            ASCII.PrintAscii();

            for (int i=0; i < MenuOptions.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"> {MenuOptions[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(MenuOptions[i]);
                }
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedIndex = selectedIndex == 0? MenuOptions.Count - 1 : selectedIndex - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedIndex = selectedIndex == MenuOptions.Count - 1? 0 : selectedIndex + 1;
            }
        }
        while (key != ConsoleKey.Enter);

        return selectedIndex;
    }
}