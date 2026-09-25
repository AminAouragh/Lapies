public class Player
{
    public string Name;
    public int HP;
    public int XP;
    public int Age;
    public const int maxXP = 3000;
    public Location currentLocation;
    public Weapon? weapon;
    public Inventory inventory;
    public int QuestsDone;
    public int EnemiesDefeated;
    public int MonstersDefeated;
    public List<Quest> Quests = [];

    public Player(string name)
    {
        Name = name;
        HP = 100;
        XP = 0;
        Age = 6;
        inventory = new Inventory(this);
        weapon = null;
        QuestsDone = 0;
        EnemiesDefeated = 0;
        MonstersDefeated = 0;
    }

    public static Player CreatePlayer() //overleggen hoe en wat met teach
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("----- Create Player -----");
        Console.ResetColor();
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

    public bool IsDead()
    {
        return HP <= 0;
    }

    public bool IsAlive()
    {
        return HP > 0;
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
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"HP: {HP}/100 ");
        Console.WriteLine($"XP: {XP}/{maxXP}");
        Console.WriteLine($"Quests done: {QuestsDone}/3");
        Console.WriteLine($"Enemies defeated: {EnemiesDefeated}/5");
        Console.WriteLine($"Monsters defeated: {MonstersDefeated}");
        Console.WriteLine();
        Console.WriteLine("Press ESC to go back");

        if (Console.ReadKey(true).Key == ConsoleKey.Escape)
        {
            Menu.Start();
        }
    }
}