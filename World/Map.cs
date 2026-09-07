public static class Map
{
    // Home = quest of thors to move to north (Ocean)
    // Grassland = player (100, but deals 10 damage) tries to fight Askeladd (1000hp) but doesnt succeed (NO QUEST)
    // Forest = quest and introduction of Leif (unc), quest = protect Leif with low HP (bandit), if player dies, try again else
    // cutscene between player and Leif (anger grown, change..) -> Leif brings Player to Town

    public static void DrawMap(Location current)
    {
        string vinland = LocationBox("VINLAND", World.Vinland, current);
        string ocean = LocationBox("OCEAN", World.Ocean, current);
        string forest = LocationBox("FOREST", World.Forest, current);
        string town = LocationBox("TOWN", World.Town, current);
        string home = LocationBox("HOME", World.Home, current);
        string iceland = LocationBox("ICELAND", World.Iceland, current);
        string canute = LocationBox("CANUTE", World.Canute, current);
        string grassland = LocationBox("GRASSLAND", World.Grassland, current);

        Console.WriteLine($@"
                                ┌───────────┐
                                │{vinland}│
                                └───────────┘
                                      ≈
                                      ≈
                                ┌───────────┐
                                │{ocean}│
                                └───────────┘
                                      ≈
                                      ≈
┌───────────┐   ┌───────────┐   ┌───────────┐   ┌───────────┐   ┌───────────┐
│{forest}│───│{town}│───│{home}│───│{iceland}│───│{canute}│
└───────────┘   └───────────┘   └───────────┘   └───────────┘   └───────────┘
                                      │
                                      │
                                ┌───────────┐
                                {grassland}
                                └───────────┘

   ▶ ◀  you are here          ≈  sea route          ─  road
");
    }

    public static string LocationBox(string location, Location box, Location current)
    {
        string text = box == current?  $"▶ {location} ◀" : location;
        return text.PadLeft((11 + text.Length) / 2).PadRight(11); //padding voor locatie namen, langste is GRASSLAND (11 karakters)
    }
}
