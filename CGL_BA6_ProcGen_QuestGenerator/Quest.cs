//(c) copyright by Martin M. Klöckener

namespace QuestGenerator;

public class Quest
{
    public List<Goal> Goals = new();

    public bool IsCompleted => Goals.All(goal => goal.IsCompleted);
}