public class World
{
    public static Player Player;
    public static bool isInitialized = false;
    public static bool thorkellQuest = false;
    public static bool askeladdQuest = false;
    public static bool Endgame = false;
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
    public static Item reward_rat = new("Poisen ring", "Gives you inmunite to poisen", 0,false,0);
    public static Item reward_snake = new("Knife", "Knife to skin snakes", 5, false,0);
    public static Item reward_monster = new("Spider eyes", "A rare prize from a deadly creature of the forest",0,false,0);
    public static string instruction = "";

    // public const int LOCATION_ID_HOME = 1;
    // public const int LOCATION_ID_TOWN_SQUARE = 2;
    // public const int LOCATION_ID_GUARD_POST = 3;
    // public const int LOCATION_ID_ALCHEMIST_HUT = 4;
    // public const int LOCATION_ID_ALCHEMISTS_GARDEN = 5;
    // public const int LOCATION_ID_FARMHOUSE = 6;
    // public const int LOCATION_ID_FARM_FIELD = 7;
    // public const int LOCATION_ID_BRIDGE = 8;
    // public const int LOCATION_ID_SPIDER_FIELD = 9;



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
                    Scenes.After_quest1_story1(player);
                    current.BeenHere = true;
                }
            }
            else if(current == Home && current.BeenHere && !current.SecondTime && second == false)
            {
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
                    sceneTriggered = true;
                    thorkellQuest = true;
                }
            }
            else if (current == Canute && !current.BeenHere)
            {
                player.Age = 16;
                Scenes.Story_the_throne(player);
                Scenes.Story_after_quest3(player);
                player.Quests.Add(current.Quest);
                bool succeededEnding = current.Quest.ProtectCanute(player);
                if (succeededEnding)
                {
                    Endgame = true;
                }
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
        if (RandomGenerator.Next(100) >= 35)
        {
            return;
        }

        Monster monster = RandomGenerator.Next(3) switch
        {
            0 => new Monster("rat", 30, 5,reward_rat),
            1 => new Monster("snake", 45, 8,reward_snake),
            _ => new Monster("giant spider", 60, 10,reward_monster)
        };

        Battlesystem.StartBattle(player, monster);
        Monster.Add_reward(player);
    }

    public static List<Monster> Spawn_Wolves()
    {
        List<Monster> wolves = new()
        {
            new Monster("Alpha Wolf", 30, 6, null),
            new Monster("Sigma Wolf", 15, 3, null),
            new Monster("Wolf 1", 10, 2, null),
            new Monster("Wolf 2", 10, 2, null),
            new Monster("Wolf 3", 10, 2, null),
            new Monster("Lone Wolf", 6, 2, null)
        };
        return wolves;
    }

    // public static void PopulateWeapons(Player player)
    // {
    //     if (!player.inventory.PlayerHasItemInInventory("Sword"))
    //     {
    //         player.inventory.AddItem(new Item("Sword", "Rusty sword", 5, true, 50));
    //     }
    // }

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
//     public static void PopulateLocations()
//     {
//         // Create each location
//         Location Home = new Location(LOCATION_ID_Home, "Home", "Your house. You really need to clean up the place.", null, null);

//         Location TownSquare = new Location(LOCATION_ID_Town_SQUARE, "Town square", "You see a fountain.", null, null);

//         Location alchemistHut = new Location(LOCATION_ID_ALCHEMIST_HUT, "Alchemist's hut", "There are many strange plants on the shelves.", null, null);
//         alchemistHut.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_ALCHEMIST_GARDEN);

//         Location alchemistsGarden = new Location(LOCATION_ID_ALCHEMISTS_GARDEN, "Alchemist's garden", "Many plants are growing here.", null, null);
//         alchemistsGarden.MonsterLivingHere = MonsterByID(MONSTER_ID_RAT);

//         Location farmhouse = new Location(LOCATION_ID_FARMHOUSE, "Farmhouse", "There is a small farmhouse, with a farmer in front.", null, null);
//         farmhouse.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_FARMERS_FIELD);

//         Location farmersField = new Location(LOCATION_ID_FARM_FIELD, "Farmer's field", "You see rows of vegetables growing here.", null, null);
//         farmersField.MonsterLivingHere = MonsterByID(MONSTER_ID_SNAKE);

//         Location guardPost = new Location(LOCATION_ID_GUARD_POST, "Guard post", "There is a large, tough-looking guard here.", null, null);

//         Location bridge = new Location(LOCATION_ID_BRIDGE, "Bridge", "A stone bridge crosses a wide river.", null, null);
//         bridge.QuestAvailableHere = QuestByID(QUEST_ID_COLLECT_SPIDER_SILK);

//         Location spiderField = new Location(LOCATION_ID_SPIDER_FIELD, "Forest", "You see spider webs covering covering the trees in this forest.", null, null);
//         spiderField.MonsterLivingHere = MonsterByID(MONSTER_ID_GIANT_SPIDER);

//         // Link the locations together
//         Home.LocationToNorth = TownSquare;

//         TownSquare.LocationToNorth = alchemistHut;
//         TownSquare.LocationToSouth = Home;
//         TownSquare.LocationToEast = guardPost;
//         TownSquare.LocationToWest = farmhouse;

//         farmhouse.LocationToEast = TownSquare;
//         farmhouse.LocationToWest = farmersField;

//         farmersField.LocationToEast = farmhouse;

//         alchemistHut.LocationToSouth = TownSquare;
//         alchemistHut.LocationToNorth = alchemistsGarden;

//         alchemistsGarden.LocationToSouth = alchemistHut;

//         guardPost.LocationToEast = bridge;
//         guardPost.LocationToWest = TownSquare;

//         bridge.LocationToWest = guardPost;
//         bridge.LocationToEast = spiderField;

//         spiderField.LocationToWest = bridge;

//         // Add the locations to the static list
//         Locations.Add(Home);
//         Locations.Add(TownSquare);
//         Locations.Add(guardPost);
//         Locations.Add(alchemistHut);
//         Locations.Add(alchemistsGarden);
//         Locations.Add(farmhouse);
//         Locations.Add(farmersField);
//         Locations.Add(bridge);
//         Locations.Add(spiderField);
//     }

//     public static Location LocationByID(int id)
//     {
//         foreach (Location location in Locations)
//         {
//             if (location.ID == id)
//             {
//                 return location;
//             }
//         }

//         return null;
//     }

//     public static Weapon WeaponByID(int id)
//     {
//         foreach (Weapon item in Weapons)
//         {
//             if (item.ID == id)
//             {
//                 return item;
//             }
//         }

//         return null;
//     }



//     public static Monster MonsterByID(int id)
//     {
//         foreach (Monster monster in Monsters)
//         {
//             if (monster.ID == id)
//             {
//                 return monster;
//             }
//         }

//         return null;
//     }

//     public static Quest QuestByID(int id)
//     {
//         foreach (Quest quest in Quests)
//         {
//             if (quest.ID == id)
//             {
//                 return quest;
//             }
//         }

//         return null;
//     }
// }
