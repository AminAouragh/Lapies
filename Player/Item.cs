public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Damage { get; set; }
    public bool IsWeapon { get; set; }
    public int Price { get; set; }

    public Item(string name, string description, int damage, bool isWeapon,  int price = 0)
    {
        Name = name;
        Description = description;
        Damage = damage;
        IsWeapon = isWeapon;
        Price = price;
    }
}