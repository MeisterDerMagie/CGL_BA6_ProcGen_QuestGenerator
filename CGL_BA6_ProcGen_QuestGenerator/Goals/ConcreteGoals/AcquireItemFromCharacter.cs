//(c) copyright by Martin M. Klöckener

namespace QuestGenerator.ConcreteGoals;

public class AcquireItemFromCharacter : Goal
{
    private Type _itemType;
    private Character _targetCharacter;
    
    public AcquireItemFromCharacter(Item item, Character character) : base($"Ask {character.Name} for a {item.Name}.")
    {
        _itemType = item.GetType();
        _targetCharacter = character;
        
        Player.AcquiredItem += AcquiredItem;
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
        
        bool acquiredFromCorrectCharacter = args.AcquiredFrom == _targetCharacter;
        if (!acquiredFromCorrectCharacter)
            return;
        
        IsCompleted = true;
        Player.AcquiredItem -= AcquiredItem;
    }
}