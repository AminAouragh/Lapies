public class NPC
{
    public string Name;
    //public string Dialogue;
    public bool IsEnemy = false;
    public int HP;

    public NPC(string name, bool isEnemy, int hp)
    {
        Name = name;
        //Dialogue = dialogue;
        IsEnemy = isEnemy;
        HP = hp;
    }
}