//(c) copyright by Martin M. Klöckener

namespace QuestGenerator;

public class Office : Place
{
    public Character Owner;

    public Office(Character owner) : base($"{owner.Name}'s office")
    {
    }
}