
using QuestGenerator;
using QuestGenerator.ConcreteGoals;

//populate world with characters
var mathias = new Character("Mathias");
World.Characters.Add(mathias);

var jonas = new Character("Jonas");
World.Characters.Add(jonas);

var bjoern = new Character("Björn");
World.Characters.Add(bjoern);

//populate world with places
var openSpace = new Place("Open Space");
openSpace.IsLocked = false;
World.Places.Add(openSpace);

var aquarium = new Place("Aquarium");
aquarium.IsLocked = false;
World.Places.Add(aquarium);

var mathiasOffice = new Office(mathias);
mathiasOffice.IsLocked = false;
World.Places.Add(mathiasOffice);

var room204 = new Place("Room 204");
World.Places.Add(room204);

var room404 = new Place("Room 404");
World.Places.Add(room404);

var room211 = new Place("Room 211");
World.Places.Add(room211);

//add items to characters inventories
mathias.OwnedItems.Add(new Key(room204));
jonas.OwnedItems.Add(new Key(room404));
bjoern.OwnedItems.Add(new Key(room211));

//add items to places
var microphone = new Microphone();
mathiasOffice.ItemsAtThisPlace.Add(microphone);

var notebook = new Notebook();
mathiasOffice.ItemsAtThisPlace.Add(notebook);

var chair1 = new Chair();
room204.ItemsAtThisPlace.Add(chair1);

var chair2 = new Chair();
room404.ItemsAtThisPlace.Add(chair2);

var chair3 = new Chair();
room211.ItemsAtThisPlace.Add(chair3);

var chair4 = new Chair();
openSpace.ItemsAtThisPlace.Add(chair4);

var chair5 = new Chair();
aquarium.ItemsAtThisPlace.Add(chair5);

var table1 = new Table();
room204.ItemsAtThisPlace.Add(table1);

var table2 = new Table();
room211.ItemsAtThisPlace.Add(table2);

var computer = new Computer();
openSpace.ItemsAtThisPlace.Add(computer);

var lectern = new Lectern();
aquarium.ItemsAtThisPlace.Add(lectern);



//Generate Quests
var questGenerator = new QuestGenerator.QuestGenerator();

string userInput = string.Empty;
while (userInput != "q")
{
    Quest quest = questGenerator.Generate();
    
    Console.WriteLine($"You have a new Quest: {quest.Goals[0].Description}");
    
    Console.WriteLine($"Quest has {quest.Goals.Count} goals.");
    int questNumber = 0;
    for (int i = quest.Goals.Count - 1; i >= 0; i--)
    {
        questNumber++;
        Goal goal = quest.Goals[i];
        Console.WriteLine($"#{questNumber}: {goal.Description}");
    }
    
    userInput = Console.ReadLine();
}
