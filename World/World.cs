public class World
{
    public static Player Player;
    public static bool isInitialized = false;
    public static bool thorkellQuest = false;
    public static bool askeladdQuest = false;
    public static bool Endgame = false;
    public static bool spawnMonsters = false;
    public static bool canDieOneMoreTime = false;
    public static readonly List<Weapon> Weapons = new List<Weapon>();
    public static readonly List<Monster> Monsters = new List<Monster>();
    public static readonly List<Quest> Quests = new List<Quest>();
    public static readonly List<Location> Locations = new List<Location>();
    public static readonly Random RandomGenerator = new Random();

    public const int WEAPON_ID_RUSTY_SWORD = 1;
    public const int WEAPON_ID_CLUB = 2;

    public const int MONSTER_ID_RAT = 1;
    public const int MONSTER_ID_SNAKE = 2;
    public const int MONSTER_ID_GIANT_SPIDER = 3;

    public const int QUEST_ID_CLEAR_ALCHEMIST_GARDEN = 1;
    public const int QUEST_ID_CLEAR_FARMERS_FIELD = 2;
    public const int QUEST_ID_COLLECT_SPIDER_SILK = 3;

    //Locations
    public static Location Home = new("Home");
    public static Location Ocean = new("Ocean");
    public static Location Grassland = new("Grassland");
    public static Location Forest = new("Forest");
    public static Location Town = new("Town");
    public static Location Iceland = new("Iceland");
    public static Location Canute = new("Canute's Kingdom");
    public static Location Vinland = new("Vinland");

    //rewards
    public static Item stale_rations = new Item("Stale Rations", "Hard bread and dried meat scavenged from a dead man's pack. Not much, but it's something.", 0, 15, false, true, 2);
    public static Item herbal_poultice = new Item("Herbal Poultice", "Crushed herbs bound in cloth, the kind camp healers use to close a wound before infection sets in.", 0, 30, false, true, 8);
    public static Item battlefield_bandages = new Item("Battlefield Bandages", "Clean linen soaked in something bitter. Whoever packed these expected to survive using them.", 0, 45, false, true, 15);

    public static Item copper_ring = new Item("Copper Ring", "A cheap ring pried off a dead man's finger. Not worth much, but coin is coin.", 0,0, false, false, 5);
    public static Item stolen_coin_purse = new Item("Stolen Coin Purse", "A leather purse, already looted once before you got to it.", 0, 0, false, false, 20);
    public static Item wolf_pelt = new Item("Wolf Pelt", "Thick, matted fur — the kind traders in any town will pay for without asking where it came from.", 0, 0, false, false, 12);
    public static string instruction = "";

    public World(Player player)
    {
        Player = player;
    }

    public static void Start(Player player)
    {
        if (!isInitialized)
        {
            PopulateLocations(player);
            PopulateNPC(player);
            PopulateQuests(player);
            isInitialized = true;
        }

        Location currentLocation = player.currentLocation ?? Home; //die "??" staat voor als player al bij home was (dus niet null), dan laatste locatie spawn
        player.currentLocation = currentLocation;
        if (!Home.BeenHere && !Home.SecondTime)
        {
            Scenes.PlayIntro();
            Scenes.Intro(player);
            Home.BeenHere = true;
        }

        ConsoleKey key;
        do
        {
            Console.Clear();
            string location = currentLocation == Home ? "You are Home" : $"You are in the {currentLocation.Name} area";
            bool followThors = currentLocation == Home && !Ocean.BeenHere;
            bool followLeif = currentLocation == Home && Home.SecondTime && !Iceland.BeenHere;
            bool thorkell = currentLocation != Canute  && !Canute.BeenHere && thorkellQuest;
            bool lapis = currentLocation != Vinland && !Vinland.BeenHere && askeladdQuest;
            instruction =
            followThors ? "Follow Thors to the Ocean" :
            followLeif ? "Follow leif to Iceland" :
            thorkell ? "Find the king" :
            lapis ? "Far to the west, Far across the ocean, Lies a place" : "";
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write($"Location: {location}");
            Console.ResetColor();
            if (instruction != "")
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"\nInstruction: {instruction}");
                Console.ResetColor();
            }
            PrintLegenda();
            PrintCompass();
            Console.SetCursorPosition(0, 2);

            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.M)
            {
                Menu.Start();
                return;
            }
            if (followThors && key != ConsoleKey.W)
            {
                Console.WriteLine("Thors did not go that way, open up your map if you're lost");
                Thread.Sleep(500);
                continue;
            }
            if (followLeif && key != ConsoleKey.D)
            {
                Console.WriteLine("Leif did not go that way, open up your map if you're lost");
                Thread.Sleep(500);
                continue;
            }
            if (spawnMonsters)
            {
                SpawnMonster(player);
            }
            if (player.IsDead() && canDieOneMoreTime)
            {
                Scenes.GameOver(player);
            }
            currentLocation = Movement(currentLocation, key);
            currentLocation = SceneInAction(player, currentLocation, currentLocation.BeenHere, currentLocation.SecondTime);
            player.currentLocation = currentLocation;
        }
        while (true);
    }

    public static Location SceneInAction(Player player, Location current, bool beenHere, bool second)
    {
        bool sceneTriggered = true;

        while (sceneTriggered)
        {
            sceneTriggered = false;

            if (current == Ocean && beenHere == false && second == false)
            {
                Scenes.PlayOceanBattleContext();
                Scenes.OceanBattle(player);
                Thread.Sleep(5000);
                Scenes.ArrowsAndMusic(player);
                Thread.Sleep(2500);
                Console.Clear();
                Scenes.ThorsDeath(player);
                current.BeenHere = true;

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"You are now going to the {Grassland.Name} area.");
                Console.ResetColor();
                Thread.Sleep(1300);

                current = Grassland;
                sceneTriggered = true;
            }
            else if (current == Grassland && beenHere == false && second == false)
            {
                Scenes.PlayPlayerAudio();
                Scenes.PlayerNPCBattle(player);
                player.Age = 8;
                Battlesystem.StartBattleNPC(player, Grassland.NPC);
                Console.Clear();
                Scenes.PlayerLossNPC(player);
                current.BeenHere = true;
                player.HP = 34;

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"You are now going to the {Forest.Name} area.");
                Console.ResetColor();
                Thread.Sleep(1300);
                current = Forest;
                sceneTriggered = true;
            }
            else if (current == Forest && !current.BeenHere && second == false)
            {
                Scenes.PlayForestAudio();
                player.Age = 12;
                Scenes.LeifIntro(player);
                sceneTriggered = true;
                current.BeenHere = true;
            }
            else if(current == Town && !current.BeenHere && second == false)
            {

                current.Quest.Protect_Leif(player);
                if (!player.Quests.Contains(current.Quest))
                {
                    player.Quests.Add(current.Quest);
                }
                if (!current.Quest.IsDone)
                {
                    current = Forest;
                }
                else
                {
                    Scenes.PlayTownAudio();
                    Scenes.After_quest1_story1(player);
                    current.BeenHere = true;
                }

            }
            else if(current == Home && current.BeenHere && !current.SecondTime && second == false)
            {
                Scenes.PlayFamilieAudio();
                player.Age = 14;
                Scenes.Story_your_mother_and_sister(player);
                player.HP = 100;
                sceneTriggered = true;
                current.SecondTime = true;
            }
            else if(current == Iceland && !current.BeenHere && second == false)
            {
                if (!player.Quests.Contains(current.Quest))
                {
                    player.Quests.Add(current.Quest);
                }
                Scenes.Story_Thorkell(player);
                sceneTriggered = true;
                current.Quest.Beat_Thorkell(player, current.NPC);
                if (!Iceland.Quest.IsDone)
                {
                    current = Home;
                }
                else
                {
                    Scenes.Story_after_quest2(player);
                    current.BeenHere = true;
                    spawnMonsters = true;
                    player.HP = 84;
                    sceneTriggered = true;
                    thorkellQuest = true;
                }
            }
            else if (current == Canute && !current.BeenHere)
            {
                player.Age = 16;
                Scenes.Story_the_throne(player);
                Scenes.PlayTheKingAudio();
                Scenes.Story_after_quest3(player);
                player.Quests.Add(current.Quest);
                bool succeededEnding = current.Quest.ProtectCanute(player);
                if (!succeededEnding)
                {
                    Endgame = false;
                    canDieOneMoreTime = true;
                }
                Endgame = true;
                Scenes.Story_the_last_conversation(player);
                askeladdQuest = true;
                current.BeenHere = true;
                sceneTriggered = true;
            }
            else if (current == Vinland && !current.BeenHere)
            {
                player.Age = 19;
                current.BeenHere = true;
                player.currentLocation = Vinland;
                sceneTriggered = true;
                spawnMonsters = false;
                Scenes.Story_after(player);
                Thread.Sleep(5000);
                Scenes.Story_the_end_lapies(player);
            }
        }
        return current;
    }

    //aparte method anders start te groot
    public static Location Movement(Location currentLocation, ConsoleKey key)
    {

        if (key != ConsoleKey.W && key != ConsoleKey.A && key != ConsoleKey.S && key != ConsoleKey.D)
        {
            return currentLocation; // dit omdat anders elke andere key voor output zorgt
        }

        Location nextLocation = key switch
        {
            ConsoleKey.W => currentLocation.North,
            ConsoleKey.S => currentLocation.South,
            ConsoleKey.D => currentLocation.East,
            ConsoleKey.A => currentLocation.West,
            _ => null
        };
        if (!Endgame && nextLocation == Vinland )
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("You haven't reached Lapis yet!");
            Console.ResetColor();
            Thread.Sleep(500);
            return currentLocation;
        }
        if (nextLocation != null)
        {
            Console.WriteLine($"You are now going to the {nextLocation.Name} area.");
            Thread.Sleep(1000);
            return nextLocation;
        }
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("There's nothing that way");
        Thread.Sleep(350); // vorige project gebruikt voor user blocked, laat alleen text zien voor zoveel miliseconden
        Console.ResetColor();
        return currentLocation;
    }

    public static void BlockInput()
    {
        while (Console.KeyAvailable)
        {
            Console.ReadKey(true);
        }
    }


    public static void PrintLegenda()
    {
        string legenda = "Open Menu -> M";
        string forward = "Move North -> W";
        string left = "Move West -> A";
        string backward = "Move South -> S";
        string right = "Move East -> D";
        Console.SetCursorPosition(Console.WindowWidth - legenda.Length, 0);
        Console.Write(legenda);
        Console.SetCursorPosition(Console.WindowWidth - forward.Length, 1);
        Console.Write(forward);
        Console.SetCursorPosition(Console.WindowWidth - left.Length, 2);
        Console.Write(left);
        Console.SetCursorPosition(Console.WindowWidth - backward.Length, 3);
        Console.Write(backward);
        Console.SetCursorPosition(Console.WindowWidth - right.Length, 4);
        Console.Write(right);
        Console.SetCursorPosition(Console.WindowWidth - right.Length, 5);
        Console.Write("+ ---------- +");
    }

    public static void PrintCompass()
    {
        int width = 9;
        int startY = Console.WindowHeight - 6;

        Console.SetCursorPosition(Console.WindowWidth - width, startY);
        Console.Write("    N");

        Console.SetCursorPosition(Console.WindowWidth - width, startY + 1);
        Console.Write("    |");

        Console.SetCursorPosition(Console.WindowWidth - width, startY + 2);
        Console.Write("W - + - E");

        Console.SetCursorPosition(Console.WindowWidth - width, startY + 3);
        Console.Write("    |");

        Console.SetCursorPosition(Console.WindowWidth - width, startY + 4);
        Console.Write("    S");
        Console.WriteLine();

    }

    public static void SpawnMonster(Player player)
    {
        if (RandomGenerator.Next(100) >= 33)
        {
            return;
        }

        Monster monster = RandomGenerator.Next(6) switch
        {
            0 => new Monster("Camp Scavenger", 27, 2, stale_rations),
            1 => new Monster("Adder", 31, 3, herbal_poultice),
            2 => new Monster("Deserter", 60, 4, battlefield_bandages),
            3 => new Monster("Camp Thief", 45, 4, copper_ring),
            4 => new Monster("Grave Robber", 15, 6, stolen_coin_purse),
            5 => new Monster("Lame Wolf", 20, 7, wolf_pelt),
            _ => null
        };
        if (monster != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nOh no! {monster.Name} spawned here!!");
            Console.ResetColor();
            Thread.Sleep(800);
            Battlesystem.StartBattle(player, monster);
            if (player.HP > 0)
            {
                Monster.Add_reward(player, monster);
            }
            return;
        }
        return;
    }

    public static List<Monster> Spawn_Wolves()
    {
        List<Monster> wolves = new()
        {
            new Monster("Alpha Wolf", 27, 6, null),
            new Monster("Sigma Wolf", 15, 3, null),
            new Monster("Wolf 1", 10, 3, null),
            new Monster("Wolf 2", 10, 2, null),
            new Monster("Wolf 3", 10, 2, null),
            new Monster("Lone Wolf", 5, 1, null)
        };
        return wolves;
    }

    public static void PopulateNPC(Player player)
    {
        NPC thors = new($"Thors ({player.Name}'s father)", false, 100);
        NPC askeladd = new("Askeladd", true, 200);
        NPC askeladd_Unbeatable = new("Askeladd", true, 1000);
        NPC thorkell = new("Thorkell", true, 150);
        NPC leif = new("Leif", false, 100);
        NPC canute = new("Canute", false, 100);

        Home.NPC = thors;
        Ocean.NPC = askeladd;
        Iceland.NPC = thorkell;
        Forest.NPC = leif;
        Canute.NPC = canute;
        Grassland.NPC = askeladd_Unbeatable;
    }

    public static void PopulateQuests(Player player)
    {
        Quest Protect_Leif = new Quest("Protect Leif", "Protect Leif from the wolves", 100, player);
        Town.Quest = Protect_Leif;

        Quest Beat_Thorkell = new Quest("Beat Thorkell", "Beat Thorkell at Iceland", 100, player);
        Iceland.Quest = Beat_Thorkell;

        Quest Protect_Canute = new Quest("Protect Canute", "Protect Canute from Askeladd", 500, player);
        Canute.Quest = Protect_Canute;
    }

    public static void PopulateLocations(Player player)
    {
        Forest.East = Town;
        Town.West = Forest;

        Town.East = Home;
        Home.West = Town;

        Home.East = Iceland;
        Iceland.West = Home;

        Iceland.East = Canute;
        Canute.West = Iceland;

        Grassland.North = Home;
        Home.South = Grassland;

        Home.North = Ocean;
        Ocean.South = Home;

        Ocean.North = Vinland;
        Vinland.South = Ocean;
    }
}