public class Monster
{
    public string Name;
    public int HP;
    public int Damage;
    public Item? Reward;

    public Monster(string name, int hp, int damage, Item? reward = null)
    {
        Name = name;
        HP = hp;
        Damage = damage;
        Reward = reward;
    }

    public void TakeDamage(int damageDone)
    {
        HP = Math.Max(0, HP - damageDone);
    }

    public static void Add_reward(Player player, Monster monster)
    {
        Console.WriteLine($"{monster.Name} dropped {monster.Reward.Name}");
        Console.WriteLine("do you want to add this reward? yes/no ");
        string answer = Console.ReadLine().ToLower();

        while(answer != "yes" && answer!= "no")
        {
        answer = Console.ReadLine().ToLower();
        }
        if(answer == "yes" && !player.inventory.items.Contains(monster.Reward))
        {
            player.inventory.AddItem(player, monster.Reward);
            return;
        }
        else if (answer == "yes" && player.inventory.items.Contains(monster.Reward))
        {
            Console.WriteLine("You already have this item in your inventory");
            Console.ReadKey(true);
        }
        else if (answer == "no")
        {
            return;
        }
        return;
    }
}