//(c) copyright by Martin M. Klöckener

namespace QuestGenerator;

public abstract class Item
{
    public Item(string name)
    {
        Name = name;
    }

    private Item()
    {
        
    }

    public string Name { get; }
}