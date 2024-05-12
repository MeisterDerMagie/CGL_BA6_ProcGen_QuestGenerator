//(c) copyright by Martin M. Klöckener

namespace QuestGenerator;

public abstract class Goal
{
    public string Description { get; }
    public bool IsCompleted { get; set; }
    public List<Requirement> Requirements { get; set; } = new();
    public List<Goal> SubGoals { get; set; } = new();

    public bool AllRequirementsAreMet => Requirements.All(requirement => (requirement.IsMet() == true));
    public bool AllSubGoalsAreCompleted => SubGoals.All(goal => goal.IsCompleted);

    protected Goal(string description)
    {
        Description = description;
    }
}