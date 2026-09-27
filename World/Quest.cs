public class Quest
{
    public string Name;
    public int Points;
    public string Description;
    Player player;
    public bool IsDone = false;
    public static List<Monster> monstersEscaped = [];

    public Quest(string name, string description, int points, Player player1)
    {
        Name = name;
        Description = description;
        Points = points;
        player = player1;
    }

    public static void DisplayQuests(Player player)
    {
        List<string> questsOptions =
        [
            "Active Quests",
            "Quests Done"
        ];
        int index = Helpers.Navigation(questsOptions);
        switch (index)
        {
            case 0:
                Console.Clear();
                DisplayActiveQuests(player);
                break;
            case 1:
                Console.Clear();
                DisplayQuestsDone(player);
                break;
        }
    }

    public static void DisplayActiveQuests(Player player)
    {
        foreach (Quest quest in player.Quests)
        {
            if (!quest.IsDone)
            {
                Console.WriteLine($"- {quest.Name}: {quest.Description}");
            }
        }
    }

    public static void DisplayQuestsDone(Player player)
    {
        foreach (Quest quest in player.Quests)
        {
            if (quest.IsDone)
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
        Console.WriteLine("\n* Leif is trapped by the wolves you need to protect him in order to continue [QUEST] *");
        Console.ReadKey(true);
        Console.ResetColor();
        List<Monster> wolves = World.Spawn_Wolves();
        foreach (Monster wolf in wolves)
        {
            Battlesystem.StartBattle(player, wolf);
            if (player.IsDead())
            {
                player.HP = 36;
                return;
            }
            else if (wolf.HP > 0)
            {
                monstersEscaped.Add(wolf);
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

    public bool ProtectCanute(Player player)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\n3 SOLDIERS ARE TRYING TO ATTACK THE PRINCE");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("* PROTECT CANUTE!!! [QUEST]*\n");
        Console.ReadKey(true);
        Console.ResetColor();
        List<NPC> soldiers =
        [
            new NPC("Soldier Ulf", true, 100),
            new NPC("Soldier Toke", true, 100),
            new NPC("Soldier Grimr", true, 200),
        ];
        foreach (NPC soldier in soldiers)
        {
            Battlesystem.StartBattleNPC(player, soldier);
            if (player.IsDead())
            {
                foreach (Item item in player.inventory.items.ToList())
                {
                    if (item.Name.Contains($"{player.Name}'s Dual Daggers"))
                    {
                        player.inventory.RemoveItem(item);
                    }
                }
                player.weapon = new Weapon(player.inventory.items[0]);
                string lost = $"Since u could not protect Canute, you'll continue to live a miserable life\nGoodluck {player.Name}...";
                player.HP = 12;
                foreach (char c in lost)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(c);
                    Thread.Sleep(80);
                }
                Console.ResetColor();
                Thread.Sleep(2000);
                return false;
            }
            player.EnemiesDefeated++;
        }
        IsDone = true;
        player.QuestsDone++;
        return true;
    }
}