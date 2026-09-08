public class NPC
{
    public string Name;
    public string Description;
    public bool IsEnemy = false;
    public int HP;

    public NPC(string name, string description, bool isEnemy, int hp)
    {
        Name = name;
        Description = description;
        IsEnemy = isEnemy;
        HP = hp;
    }

    public void Dialogue()
    {
        Console.WriteLine(Description);
    }
}