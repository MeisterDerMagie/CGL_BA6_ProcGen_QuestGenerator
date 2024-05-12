//(c) copyright by Martin M. Klöckener

namespace QuestGenerator;

public class Character : ItemSource
{
    public string Name { get; }
    public bool Alive { get; set; } = true;
    public Place CurrentPlace { get; set; }
    public List<Item> OwnedItems { get; set; } = new();
    
    public Character(string name)
    {
        Name = name;
    }
}