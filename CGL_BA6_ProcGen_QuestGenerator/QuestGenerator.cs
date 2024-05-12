//(c) copyright by Martin M. Klöckener

using QuestGenerator.ConcreteGoals;

namespace QuestGenerator;

public class QuestGenerator
{
    private readonly Random _rng;

    public QuestGenerator()
    {
        _rng = new Random();
    }
    
    public Quest Generate()
    {
        var quest = new Quest();
        
        //generate and add main goal
        Goal mainGoal = PickRandomGoal();
        quest.Goals.Add(mainGoal);

        //check all requirement goals and add those as sub-goals
        foreach (Goal requirementGoal in GetRequirementGoals(mainGoal))
        {
            quest.Goals.Add(requirementGoal);
        }
        
        //check all actual sub goals and add those
        //List<Goal> subGoals = new List<Goal>();
        //for (int i = quest.Goals.Count - 1; i >= 0; i--)
        //{
        //    Goal goal = quest.Goals[i];
        //    subGoals.AddRange(goal.SubGoals);
        //}
        //quest.Goals.InsertRange(0, subGoals);
        
        return quest;
    }

    private Goal PickRandomGoal()
    {
        Func<Goal>[] allPossibleGoals =
        {
            () =>
            {
                var character = PickRandomCharacter();
                var item = PickRandomItemFromCharacter(character);
                return new AcquireItemFromCharacter(item, character);
            },
            () =>
            {
                var itemAndPlace = PickRandomItemFromRandomPlace();
                return new AcquireItemFromPlace(itemAndPlace.item, itemAndPlace.place);
            },
            () => new AirOutRoom(PickRandomPlace(), PickRandomCharacter()),
            () => new PrepareHybridSetup(PickRandomPlace()),
            () => new EscortCharacter(PickRandomCharacter(), PickRandomPlace()),
            () => new DeliverItemToCharacter(PickRandomCharacter(), PickRandomItemFromRandomPlace().item)
        };
        
        int randomIndex = _rng.Next(allPossibleGoals.Length);
        return allPossibleGoals[randomIndex]();
    }

    private List<Goal> GetRequirementGoals(Goal parentGoal)
    {
        var subGoals = new List<Goal>();

        foreach (Requirement requirement in parentGoal.Requirements)
        {
            subGoals.Add(PickRandomRequirementResolution(requirement.RequirementResolutions));
            
            //get subgoals recursively
            subGoals.AddRange(GetRequirementGoals(PickRandomRequirementResolution(requirement.RequirementResolutions)));
        }

        return subGoals;
    }

    private Character PickRandomCharacter()
    {
        int randomIndex = _rng.Next(World.Characters.Count);
        return World.Characters[randomIndex];
    }

    private Place PickRandomPlace()
    {
        var unvisitedPlaces = World.Places.Where(place => !place.IsVisited).ToList();
        if (unvisitedPlaces.Count > 0)
        {
            int randomIndex = _rng.Next(unvisitedPlaces.Count);
            return unvisitedPlaces[randomIndex];
        }
        else
        {
            int randomIndex = _rng.Next(World.Places.Count);
            return World.Places[randomIndex];
        }
    }
    
    private Goal PickRandomRequirementResolution(List<Goal> resolutions)
    {
        int randomIndex = _rng.Next(resolutions.Count);
        return resolutions[randomIndex];
    }
    
    private Item PickRandomItemFromCharacter(Character character)
    {
        int randomIndex = _rng.Next(character.OwnedItems.Count);
        return character.OwnedItems[randomIndex];
    }
    
    private (Item item, Place place) PickRandomItemFromRandomPlace()
    {
        var placesWithItems = World.Places.Where(place => place.ItemsAtThisPlace.Count > 0).ToList();
        
        var unvisitedPlacesWithItems = placesWithItems.Where(place => !place.IsVisited).ToList();

        Place randomPlace;
        if (unvisitedPlacesWithItems.Count > 0)
        {
            int randomIndex = _rng.Next(unvisitedPlacesWithItems.Count);
            randomPlace = unvisitedPlacesWithItems[randomIndex];
        }
        else
        {
            int randomIndex = _rng.Next(placesWithItems.Count);
            randomPlace = placesWithItems[randomIndex];
        }

        int randomItemIndex = _rng.Next(randomPlace.ItemsAtThisPlace.Count);
        return (randomPlace.ItemsAtThisPlace[randomItemIndex], randomPlace);
    }
}