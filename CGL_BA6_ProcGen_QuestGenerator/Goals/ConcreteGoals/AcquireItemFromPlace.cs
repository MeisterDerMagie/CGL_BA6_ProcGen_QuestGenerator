//(c) copyright by Martin M. Klöckener

namespace QuestGenerator.ConcreteGoals;

public class AcquireItemFromPlace : Goal
{
    private Type _itemType;
    private Place _targetPlace;
    
    public AcquireItemFromPlace(Item item, Place place) : base($"Acquire {item.Name} from {place.Name}.")
    {
        _itemType = item.GetType();
        _targetPlace = place;
        
        Player.AcquiredItem += AcquiredItem;
        
        //requires the place to be unlocked
        if (_targetPlace.IsLocked)
        {
            var requirementResolutions = new List<Goal>() { new UnlockPlace(_targetPlace) };
            var havePlaceUnlocked = new Requirement($"Unlock {_targetPlace.Name}", () => !_targetPlace.IsLocked, requirementResolutions);
            Requirements.Add(havePlaceUnlocked);
        }
    }

    private void AcquiredItem(Player.ItemAcquisitionArgs args)
    {
        if (!AllRequirementsAreMet)
            return;
        
        if (!AllSubGoalsAreCompleted)
            return;
        
        bool isCorrectItemType = args.Item.GetType() == _itemType;
        if (!isCorrectItemType)
            return;
        
        bool acquiredFromCorrectCharacter = args.AcquiredFrom == _targetPlace;
        if (!acquiredFromCorrectCharacter)
            return;
        
        IsCompleted = true;
    }
}