//(c) copyright by Martin M. Klöckener

namespace QuestGenerator;

public class Place : ItemSource
{
    public string Name { get; }
    public bool IsVisited { get; set; }
    public bool IsLocked { get; set; } = true;
    public List<Item> ItemsAtThisPlace { get; set; } = new();
    
    public Place(string name)
    {
        Name = name;
    }
}