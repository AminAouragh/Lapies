public static class Map
{
    public static void DrawMap()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        string home = "Home";
        Console.WriteLine($@"
                                   +--------------------+
                                   | Alchemist's Garden |
                                   +--------------------+
                                             |
                                             |
                                   +--------------------+
                                   |  Alchemist's Hut   |
                                   +--------------------+
                                             |
                                             |
+----------------+   +-----------+   +---------------+   +------------+   +--------+   +--------+
| Farmer's Field |---| Farmhouse |---|      {home}     |---| Guard Post |---| Bridge |---| Forest |
+----------------+   +-----------+   +---------------+   +------------+   +--------+   +--------+
                                             |
                                             |
                                   +--------------------+
                                   |     Town Square    |
                                   +--------------------+
");
    }
}
