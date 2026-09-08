public class World
{
    public Player Player;
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
        // PopulateWeapons();
        // PopulateMonsters();
        // PopulateQuests();
        // PopulateLocations();

    }

    public static void Start(Player player)
    {
        //movement van punt a -> b
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

        //test movement
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(Home.Name);
        Console.ResetColor();
        Location currentLocation = Home;
        player.currentLocation = currentLocation;

        ConsoleKey key;
        do
        {
            Console.Clear();
            Console.WriteLine($"Greetings, {player.Name}\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("GOAL OF GAME: Reach Lapis\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You are in the {currentLocation.Name} area.");
            Console.ResetColor();
            Console.WriteLine();

            PrintLegenda();
            PrintCompass();
            Console.SetCursorPosition(0, 11);

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.M)
            {
                Menu.Start();
                return;
            }
            currentLocation = Movement(currentLocation, key);
            player.currentLocation = currentLocation;
        }
        while (true);
    }

/* mijn idee, miss list met random monsters en dan random nummer door random laten generaten en
gebruiken als index voor list

*/

    //aparte method anders start te groot
    public static Location Movement(Location currentLocation, ConsoleKey key)
    {
        Location nextLocation = key switch
        {
            ConsoleKey.W => currentLocation.North,
            ConsoleKey.S => currentLocation.South,
            ConsoleKey.D => currentLocation.East,
            ConsoleKey.A => currentLocation.West,
            _ => null
        };

        if (nextLocation != null)
        {
            Console.WriteLine($"You are now going to the {nextLocation.Name} area.");
            Console.ReadKey(true);
            return nextLocation;
        }
        return currentLocation;
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
            0 => new Monster("rat", 30, 5),
            1 => new Monster("snake", 45, 8),
            _ => new Monster("giant spider", 60, 10)
        };

        Battlesystem.StartBattle(player, monster);
    }
}


//     public static void PopulateWeapons()
//     {
//         Weapons.Add(new Weapon(WEAPON_ID_RUSTY_SWORD, "Rusty sword", 5));
//         Weapons.Add(new Weapon(WEAPON_ID_CLUB, "Club", 10));
//     }

//     public static void PopulateMonsters()
//     {
//         Monster rat = new Monster(MONSTER_ID_RAT, "rat", 1, 3, 3);


//         Monster snake = new Monster(MONSTER_ID_SNAKE, "snake", 10, 7, 7);


//         Monster giantSpider = new Monster(MONSTER_ID_GIANT_SPIDER, "giant spider", 3, 10, 10);


//         Monsters.Add(rat);
//         Monsters.Add(snake);
//         Monsters.Add(giantSpider);
//     }

//     public static void PopulateQuests()
//     {
//         Quest clearAlchemistGarden =
//             new Quest(
//                 QUEST_ID_CLEAR_ALCHEMIST_GARDEN,
//                 "Clear the alchemist's garden",
//                 "Kill rats in the alchemist's garden ");



//         Quest clearFarmersField =
//             new Quest(
//                 QUEST_ID_CLEAR_FARMERS_FIELD,
//                 "Clear the farmer's field",
//                 "Kill snakes in the farmer's field");


//         Quest clearSpidersForest =
//                     new Quest(
//                         QUEST_ID_COLLECT_SPIDER_SILK,
//                         "Collect spider silk",
//                         "Kill spiders in the spider forest");


//         Quests.Add(clearAlchemistGarden);
//         Quests.Add(clearFarmersField);
//         Quests.Add(clearSpidersForest);
//     }

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
