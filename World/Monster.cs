public class Monster
{
    public string Name;
    public int HP;
    public int Damage;
    public static Item? Reward;

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

    public static void Add_reward(Player player)
    {
        Console.WriteLine("do you want to add this reward? yes/no ");
        string answer = Console.ReadLine().ToLower();

        while(answer != "yes" && answer!= "no")
        {
        answer = Console.ReadLine().ToLower();
        }
        if(answer == "yes")
        {
            player.inventory.AddItem(Reward);
            return;
        }
        else
        {
            return;
        }

    }
}