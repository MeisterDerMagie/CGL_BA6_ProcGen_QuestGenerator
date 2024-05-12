//(c) copyright by Martin M. Klöckener

namespace QuestGenerator.ConcreteGoals;

public class PrepareHybridSetup : Goal
{
    private Place _place;
    
    public PrepareHybridSetup(Place place) : base($"Prepare the hybrid setup in room {place.Name}.")
    {
        _place = place;

        Player.PreparedHybridSetup += CheckForCompletion;
        
        //requirement: unlock room
        if (_place.IsLocked)
        {
            var requirementResolutionsUnlockRoom = new List<Goal>() { new UnlockPlace(_place) };
            var havePlaceUnlocked = new Requirement($"Unlock {_place.Name}.", () => !_place.IsLocked, requirementResolutionsUnlockRoom);
            Requirements.Add(havePlaceUnlocked);
        }
        
        //requirement: have a notebook
        var requirementResolutionsHaveNotebook = new List<Goal>() { new AcquireItem(new Notebook()) };
        var haveNotebook = new Requirement($"Acquire a notebook.", () => Player.PlayerInventory.Any(item => item is Notebook), requirementResolutionsHaveNotebook);
        Requirements.Add(haveNotebook);
        
        //requirement: have a microphone
        var requirementResolutionsHaveMicrophone = new List<Goal>() { new AcquireItem(new Microphone()) };
        var haveMicrophone = new Requirement($"Acquire a microphone.", () => Player.PlayerInventory.Any(item => item is Microphone), requirementResolutionsHaveMicrophone);
        Requirements.Add(haveMicrophone);
    }

    private void CheckForCompletion(Place place)
    {
        if (!AllRequirementsAreMet)
            return;
        
        if (!AllSubGoalsAreCompleted)
            return;

        if (place != _place)
            return;

        IsCompleted = true;
        Player.PreparedHybridSetup -= CheckForCompletion;
    }
}