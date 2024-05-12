//(c) copyright by Martin M. Klöckener

namespace QuestGenerator;

public static class World
{
    public static List<Character> Characters = new();
    public static List<Item> Items = new();
    public static List<Place> Places = new();

    public static bool TryGetCharacterWithItem(Item item, out Character character)
    {
        foreach (Character chara in Characters)
        {
            foreach (Item ownedItem in chara.OwnedItems)
            {
                bool hasItem = ownedItem.GetType() == item.GetType();
                if (hasItem)
                {
                    character = chara;
                    return true;
                }
            }
        }

        character = null;
        return false;
    }

    public static bool TryGetPlaceWithItem(Item item, out Place place)
    {
        foreach (Place plc in Places)
        {
            foreach (Item ownedItem in plc.ItemsAtThisPlace)
            {
                bool hasItem = ownedItem.GetType() == item.GetType();
                if (hasItem)
                {
                    place = plc;
                    return true;
                }
            }
        }

        place = null;
        return false;
    }
}