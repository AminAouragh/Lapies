public class Weapon
{
    public int Damage;
    public Random random = new();

    public Weapon(int damage)
    {
        Damage = damage;
    }

    public int DealDamage(int minimalDamage, int maximalDamage)
    {
        return random.Next(minimalDamage, maximalDamage);
    }
}