//(c) copyright by Martin M. Klöckener

namespace QuestGenerator;

public static class Player
{
    public static event Action<Character> RevivedCharacter = delegate(Character revivedCharacter) {  };

    public static event Action<ItemAcquisitionArgs> AcquiredItem = delegate(ItemAcquisitionArgs acquisitionArgs) {  };
    public struct ItemAcquisitionArgs
    {
        public Item Item;
        public ItemSource AcquiredFrom;
    }

    public static event Action<ItemDeliveryArgs> DeliveredItem = delegate(ItemDeliveryArgs deliveryArgs) {  };
    public struct ItemDeliveryArgs
    {
        public Item Item;
        public ItemSource DeliveredTo;
    }
    
    public static event Action<Place> EnteredPlace = delegate(Place place) {  };
    
    public static event Action<Place> AiredOutRoom = delegate(Place room) {  };
    
    public static event Action<Place> PreparedHybridSetup = delegate(Place place) {  };
    
    public static event Action<Character> TalkedToCharacter = delegate(Character character) {  };
    
    
    public static List<Item> PlayerInventory = new();

    public static void AddItemToInventory(Item item)
    {
        PlayerInventory.Add(item);
        AcquiredItem?.Invoke(new ItemAcquisitionArgs() {Item = item});
    }
}