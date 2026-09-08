public class Monster
{
    public string Name;
    public int HP;
    public int Damage;

    public Monster(string name, int hp, int damage = 8)
    {
        Name = name;
        HP = hp;
        Damage = damage;
    }

    public void TakeDamage(int damageDone)
    {
        HP = Math.Max(0, HP - damageDone);
    }
}