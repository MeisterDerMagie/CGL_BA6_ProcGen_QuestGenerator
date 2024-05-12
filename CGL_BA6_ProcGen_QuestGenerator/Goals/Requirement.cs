//(c) copyright by Martin M. Klöckener

namespace QuestGenerator;

public class Requirement
{
    public string Description { get; }
    public Func<bool> IsMet { get; }
    public List<Goal> RequirementResolutions { get; }
    
    public Requirement(string description, Func<bool> isMet, List<Goal> requirementResolutions)
    {
        RequirementResolutions = requirementResolutions;
        Description = description;
        IsMet = isMet;
    }
}