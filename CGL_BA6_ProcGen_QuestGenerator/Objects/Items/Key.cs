//(c) copyright by Martin M. Klöckener

namespace QuestGenerator;

public class Key : Item
{
    public Place Unlocks;
    
    public Key(Place placeToUnlock) : base($"Key to {placeToUnlock.Name}")
    {
        Unlocks = placeToUnlock;
    }
}