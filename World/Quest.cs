public class Quest
{
    public string Name;
    public int Points;
    public string Description;
    Player player;
    public bool IsDone = false;

    public Quest(string name, string description, int points, Player player1)
    {
        Name = name;
        Description = description;
        Points = points;
        player = player1;
    }

    public static void DisplayQuests()
    {
        foreach (Quest quest in World.Quests)
        {
            if (!quest.IsDone)
            {
                Console.WriteLine($"- {quest.Name}: {quest.Description}");
            }
        }
    }

    public void Protect_Leif(Player player)
    {
        Console.WriteLine($"\nLEIF: AAGGHHH WOLVES {player.Name.ToUpper()}! WOLVESS..");
        Thread.Sleep(1800);
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Leif is trapped by the wolves you need to protect him\nin order to continue *QUEST*");
        Console.ReadKey(true);
        Console.ResetColor();
        List<Monster> wolves = World.Spawn_Wolves();
        foreach (Monster wolf in wolves)
        {
            Battlesystem.StartBattle(player, wolf);
            if (player.IsDead())
            {
                player.HP = 34;
                return;
            }
        }
        IsDone = true;
        player.QuestsDone++;
    }

    public void Beat_Thorkell(Player player, NPC thorkell)
    {
        Battlesystem.StartBattleNPC(player, thorkell);
        if (player.IsDead())
        {
            player.HP = 100;
            thorkell.HP = 150;
            return;
        }
        IsDone = true;
        player.EnemiesDefeated++;
        player.QuestsDone++;
    }
}