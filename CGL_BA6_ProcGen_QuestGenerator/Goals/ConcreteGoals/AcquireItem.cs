//(c) copyright by Martin M. Klöckener

namespace QuestGenerator.ConcreteGoals;

public class AcquireItem : Goal
{
    private Type _itemType;
    
    public AcquireItem(Item item) : base($"Acquire {item.Name}")
    {
        _itemType = item.GetType();
        
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
        
        IsCompleted = true;
        Player.AcquiredItem -= AcquiredItem;
    }
}