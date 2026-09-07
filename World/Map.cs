public static class Map
{
    // Home = quest of thors to move to north (Ocean)
    // Grassland = player (100, but deals 10 damage) tries to fight Askeladd (1000hp) but doesnt succeed (NO QUEST)
    // Forest = quest and introduction of Leif (unc), quest = protect Leif with low HP (bandit), if player dies, try again else
    // cutscene between player and Leif (anger grown, change..) -> Leif brings Player to Town

    public static void DrawMap()
    {
        string home = "Home";
        Console.WriteLine($@"

                                      +--------+
                                      | VinLand|
                                      +---------
                                            |
                                            |
                                   +--------------------+
                                   |       Ocean        |
                                   +--------------------+
                                             |
                                             |
+----------------+   +-----------+   +---------------+   +------------+   +--------+
|     Forest     |---|    Town   |---|      {home}     |---|   Iceland  |---| Canute
+----------------+   +-----------+   +---------------+   +------------+   +--------+
                                             |
                                             |
                                   +--------------------+
                                   |      Grassland     |
                                   +--------------------+
");
    }
}
