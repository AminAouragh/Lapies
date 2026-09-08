public class Player
{
    // als iets const is dan hoef/kan je niet opvragen aan player toch?
    // hoe kan je const gebruiken om het als vaste begin waarde te laten maar naar mate meer spelen, meer xp en/of quests done
    public string Name;
    public int HP;
    public int XP;
    public const int maxXP = 3000;
    public Location currentLocation;
    public Weapon weapon;
    public Inventory inventory;
    public int QuestsDone;
    public int EnemiesDefeated;

    public Player(string name)
    {
        Name = name;
        HP = 100;
        XP = 0;
        inventory = new Inventory(this);
        weapon = new Weapon(10);
        QuestsDone = 0;
        EnemiesDefeated = 0;
    }

    public static Player CreatePlayer() //overleggen hoe en wat met teach
    {
        Console.WriteLine("----- Create Player -----");
        string username;

        do
        {
            Console.WriteLine("Username");
            Console.Write("> ");
            username = Console.ReadLine();
        }
        while (string.IsNullOrEmpty(username));

        Player player = new Player(username);
        return player;
    }

    public void Move(string direction)
    {
        Location? nextLocation = null;

        switch (direction.ToLower())
        {
            case "north":
                nextLocation = currentLocation.North;
                break;
            case "south":
                nextLocation = currentLocation.South;
                break;
            case "east":
                nextLocation = currentLocation.East;
                break;
            case "west":
                nextLocation = currentLocation.West;
                break;
        }

        if (nextLocation != null)
        {
            currentLocation = nextLocation;
            Console.WriteLine($"Je bent naar {currentLocation.Name} gelopen.");
        }
    }

    public bool IsDead()
    {
        return HP <= 0;
    }

    public void TakeDamage(int damageDone)
    {
        HP -= damageDone;
    }

    public void EarnXP(int earnedXP)
    {
        XP += earnedXP;
    }

    public void SeeStats()
    {
        Console.WriteLine($"Username: {Name}");
        Console.WriteLine($"HP: {HP}/100 ");
        Console.WriteLine($"XP: {XP}/{maxXP}");
        Console.WriteLine($"Quests done: {QuestsDone}/3");
        Console.WriteLine($"Enemies defeated: {EnemiesDefeated}/5");
        Console.WriteLine();
        Console.WriteLine("Press ESC to go back");

        if (Console.ReadKey(true).Key == ConsoleKey.Escape)
        {
            Menu.Start();
        }
    }
}