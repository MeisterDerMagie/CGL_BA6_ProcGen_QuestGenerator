//(c) copyright by Martin M. Klöckener

namespace QuestGenerator.ConcreteGoals;

public class AirOutRoom : Goal
{
    private Place _place;
    
    public AirOutRoom(Place room, Character questGiver) : base($"Air out {room.Name}")
    {
        _place = room;

        Player.AiredOutRoom += CheckForCompletion;
        
        //requires the place to be unlocked
        if (_place.IsLocked)
        {
            var requirementResolutions = new List<Goal>() { new UnlockPlace(_place) };
            var havePlaceUnlocked = new Requirement($"Unlock {_place.Name}", () => !_place.IsLocked, requirementResolutions);
            Requirements.Add(havePlaceUnlocked);
        }
        
        //return to quest giver after airing out
        SubGoals.Add(new Report(questGiver));
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
        Player.AiredOutRoom -= CheckForCompletion;
    }
}