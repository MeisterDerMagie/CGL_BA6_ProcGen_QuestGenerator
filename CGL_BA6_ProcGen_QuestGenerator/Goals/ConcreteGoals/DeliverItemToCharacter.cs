//(c) copyright by Martin M. Klöckener

namespace QuestGenerator.ConcreteGoals;

public class DeliverItemToCharacter : Goal
{
    private Character _targetCharacter;
    private Type _itemType;
    
    public DeliverItemToCharacter(Character character, Item item) : base($"Deliver a {item.Name} to {character.Name}")
    {
        _targetCharacter = character;
        _itemType = item.GetType();

        Player.DeliveredItem += CheckForCompletion;
        
        //requirement: player has item
        dynamic itemInstance = Activator.CreateInstance(_itemType); //this instance is only required to set the description of the item. It's discarded after construction of the goal.
        bool anyCharacterHasItem = World.TryGetCharacterWithItem(item, out Character characterWithItem);
        bool anyPlaceHasItem = World.TryGetPlaceWithItem(item, out Place placeWithItem);
        List<Goal> requirementResolutions;
        if (anyCharacterHasItem)
        {
            requirementResolutions = new List<Goal>() { new AcquireItemFromCharacter(itemInstance, characterWithItem) };
        }
        else if (anyPlaceHasItem)
        {
            requirementResolutions = new List<Goal>() { new AcquireItemFromPlace(itemInstance, placeWithItem) };
        }
        else
        {
            throw new ArgumentException($"No character or place has a {itemInstance.Name} item.");
        }
        
        var haveItem = new Requirement($"Acquire a {itemInstance.Name}", () => Player.PlayerInventory.Any(item => item.GetType() == _itemType), requirementResolutions);
        Requirements.Add(haveItem);
    }

    private void CheckForCompletion(Player.ItemDeliveryArgs args)
    {
        if (!AllRequirementsAreMet)
            return;
        
        if (!AllSubGoalsAreCompleted)
            return;

        if (args.Item.GetType() != _itemType)
            return;

        if (_targetCharacter != args.DeliveredTo)
            return;

        IsCompleted = true;
        Player.DeliveredItem += CheckForCompletion;
    }
}