using DataStructures.Apps.Lists;
using DataStructures.Apps.Stacks;
using DataStructures.Apps.Queues;

string[] datastructures = {
    "DYNAMICARRAY",
    "SINGLYLINKEDLIST",
    "DOUBLYLINKEDLIST",
    "ARRAYSTACK",
    "NODESTACK",
    "ARRAYQUEUE",
    "NODEQUEUE"
};

if(args.Length <= 0)
{
    Console.WriteLine("Error: available Data Structures are:\n");
    foreach(var datastructure in datastructures)
    {
        Console.WriteLine($"\t{datastructure}");
    }
}
else
{
    var datastructure = args[0].ToUpper();

    switch(datastructure)
    {
        case "DYNAMICARRAY":        DynamicArrayApp.Run();      break;
        case "SINGLYLINKEDLIST":    SinglyLinkedListApp.Run();  break;
        case "DOUBLYLINKEDLIST":    DoublyLinkedListApp.Run();  break;
        case "ARRAYSTACK":          ArrayStackApp.Run();        break;
        case "NODESTACK":           NodeStackApp.Run();         break;
        case "ARRAYQUEUE":          ArrayQueueApp.Run();        break;
        case "NODEQUEUE":           NodeQueueApp.Run();         break;
        default:
            Console.WriteLine("Invalid Option! Try Again...");
            break;
    }
}