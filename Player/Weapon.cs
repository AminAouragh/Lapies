public class Weapon
{
    public Item Item;
    public int Damage => Item.Damage;
    public string Name => Item.Name;
    public Random random = new();

    public Weapon(Item item)
    {
        Item = item;
    }

    public int DealDamage(int damageWeapon)
    {
        return damageWeapon;
    }
}