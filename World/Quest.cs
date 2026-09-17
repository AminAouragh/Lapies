public class Quest
{
    public string Name;
    public int Points;
    Player player;
    public static bool IsDone = false;

    public Quest(string name, int points, Player player1)
    {
        Name = name;
        Points = points;
        player = player1;
    }

    // public void DisplayQuests(Player player)
    // {
    //     Console.WriteLine($"- Prote");
    // }

    public void Protect_Leif(Player player)
    {
        List<Monster> wolves = World.Spawn_Wolves();
        Battlesystem.StartBattle(player, wolves[0]);
        Battlesystem.StartBattle(player, wolves[1]);
        Battlesystem.StartBattle(player, wolves[2]);
        Battlesystem.StartBattle(player, wolves[3]);
        Battlesystem.StartBattle(player, wolves[4]);
        Battlesystem.StartBattle(player, wolves[5]);
        player.QuestsDone++;
    }

    public void Beat_Thorkell(Player player, NPC thorkell)
    {
        Battlesystem.StartBattleNPC(player, thorkell);
        player.QuestsDone++;
    }
}