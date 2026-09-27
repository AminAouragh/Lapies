public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Damage { get; set; }
    public int Heal {get; set;}
    public bool IsWeapon { get; set; }
    public bool IsHealing {get; set;}
    public int Price { get; set; }

    public Item(string name, string description, int damage, int heal, bool isWeapon, bool isHealing, int price = 0)
    {
        Name = name;
        Description = description;
        Damage = damage;
        Heal = heal;
        IsWeapon = isWeapon;
        IsHealing = isHealing;
        Price = price;
    }
}