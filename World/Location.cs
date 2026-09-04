public class Location
{
    public string Name;
    public Location? North;
    public Location? South;
    public Location? East;
    public Location? West;
    public bool IsQuest;


    public Location(string name)
    {
        Name = name;
    }
}