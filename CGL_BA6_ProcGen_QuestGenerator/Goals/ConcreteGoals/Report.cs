//(c) copyright by Martin M. Klöckener

namespace QuestGenerator.ConcreteGoals;

public class Report : Goal
{
    private Character _targetCharacter;
    
    public Report(Character character) : base($"Make a report to {character.Name}.")
    {
        _targetCharacter = character;

        Player.TalkedToCharacter += CheckForCompletion;
    }

    private void CheckForCompletion(Character character)
    {
        if (!AllRequirementsAreMet)
            return;
        
        if (!AllSubGoalsAreCompleted)
            return;

        if (character != _targetCharacter)
            return;

        IsCompleted = true;
        Player.TalkedToCharacter -= CheckForCompletion;
    }
}