public class NPC
{
    public string Name;
    //public string Dialogue;
    public bool IsEnemy = false;
    public int HP;
    public Random random = new();

    public NPC(string name, bool isEnemy, int hp)
    {
        Name = name;
        IsEnemy = isEnemy;
        HP = hp;
    }

    public int DealDamage(int minimalDamage, int maximalDamage)
    {
        return random.Next(minimalDamage, maximalDamage);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
    }
}