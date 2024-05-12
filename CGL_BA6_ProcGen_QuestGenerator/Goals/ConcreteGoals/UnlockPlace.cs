//(c) copyright by Martin M. Klöckener

namespace QuestGenerator.ConcreteGoals;

public class UnlockPlace : Goal
{
    private Place _place;
    
    public UnlockPlace(Place place) : base($"Unlock {place.Name}")
    {
        _place = place;

        Player.AcquiredItem += CheckForCompletion;
        
        //requirement: acquire key
        Character keyOwner = World.Characters.FirstOrDefault(character => character.OwnedItems.Any(IsRightKey));

        if (keyOwner == null)
            throw new ArgumentException($"Someone needs to have the key to {_place.Name}, otherwise we can't unlock it...");
        
        var requirementResolutions = new List<Goal>() { new AcquireItemFromCharacter(new Key(_place), keyOwner) };
        var acquireKey = new Requirement($"Acquire the key to {_place.Name} from {keyOwner.Name}", () => Player.PlayerInventory.Any(IsRightKey), requirementResolutions);
        Requirements.Add(acquireKey);
    }

    private void CheckForCompletion(Player.ItemAcquisitionArgs args)
    {
        if (!AllRequirementsAreMet)
            return;
        
        if (!AllSubGoalsAreCompleted)
            return;

        if (args.Item is not Key key)
            return;

        if (key.Unlocks != _place)
            return;

        IsCompleted = true;
        Player.AcquiredItem -= CheckForCompletion;
    }

    private bool IsRightKey(Item item)
    {
        bool isRightKey = item is Key key && key.Unlocks == _place;
        return isRightKey;
    }
}