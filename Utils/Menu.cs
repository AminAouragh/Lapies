public static class Menu
{
    public static bool IsPlaying = false;
    public static Player? player = null;

    public static void Start()
    {
        bool inMenu = true;
        do
        {
            //bool IsPlaying = false;
            string statusPlay = IsPlaying == true? "Continue Playing" : "Start Game";
            List<string> GameMenu =
            [
                statusPlay,
                "Inventory",
                "Map",
                "Quests",
                "Stats",
                "Quit"
            ];

            int indexOfChosen = Helpers.Navigation(GameMenu);

            switch (indexOfChosen) // ik kan dit in aparte method zetten nog..
            {
                case 0:
                    Console.Clear();
                    IsPlaying = true;
                    if (player is null)
                    {
                        player = Player.CreatePlayer();
                    }
                    inMenu = false;
                    break;
                case 1:
                    if (player is null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Can't access without starting a game");
                        Console.ResetColor();
                        Console.ReadKey(true);
                        break;
                    }
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Inventory opened");
                    player.inventory.Open();
                    Console.ResetColor();
                    Console.ReadKey(true);

                    break;
                case 2:
                    if (player is null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Can't access without starting a game");
                        Console.ResetColor();
                         Console.ReadKey(true);

                        break;
                    }
                    Console.Clear();
                    Map.DrawMap(player.currentLocation);
                    Console.ReadKey(true);
                    break;
                case 3:
                    if (player is null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Can't access without starting a game");
                        Console.ResetColor();
                        Console.ReadKey(true);
                        break;
                    }
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Displaying quests");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    break;
                case 4:
                    if (player is null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Can't access without starting a game");
                        Console.ResetColor();
                        Console.ReadKey(true);

                        break;
                    }
                    Console.Clear();
                    player.SeeStats();
                    //Console.ForegroundColor = ConsoleColor.DarkRed;
                    //Console.WriteLine("Displaying player stats");
                    //Console.ResetColor();
                    break;
                case 5:
                    IsPlaying = false;
                    Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("not an option?");
                    Console.ResetColor();
                    break;
            }
        } while (inMenu);
        World.Start(player);
    }
}