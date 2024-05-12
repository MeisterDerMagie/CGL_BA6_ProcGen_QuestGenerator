//(c) copyright by Martin M. Klöckener

namespace QuestGenerator.ConcreteGoals;

public class EscortCharacter : Goal
{
    private Character _targetCharacter;
    private Place _destination;
    
    public EscortCharacter(Character character, Place destination) : base($"Escort {character.Name} to {destination.Name}")
    {
        _targetCharacter = character;
        _destination = destination;

        Player.EnteredPlace += CheckForCompletion;
    }

    private void CheckForCompletion(Place place)
    {
        if (!AllRequirementsAreMet)
            return;
        
        if (!AllSubGoalsAreCompleted)
            return;

        if (place != _destination)
            return;

        if (_targetCharacter.CurrentPlace != _destination)
            return;

        IsCompleted = true;
        Player.EnteredPlace -= CheckForCompletion;
    }
}