using DataStructures.Queues;

namespace DataStructures.Apps.Queues;

public static class NodeQueueApp
{
    private static readonly int RANGE_VALUES = 10;
    private static readonly int NUMBER_SIMULATIONS = 25;
    public static void Run()
    {
        // initialize an empty Queue
        var myNodeQueue = new NodeQueue<int>();

        Console.Write("Empty Queue: ");
        Console.WriteLine(myNodeQueue);

        // simulate insertion and deletion on queue
        Random random = new Random();
        for (int i = 0; i < NUMBER_SIMULATIONS; i++)
        {
            int operation = random.Next(3);
            if(operation == 0)
            {
                try
                {
                    // removing values from the queue
                    var elementOnFront = myNodeQueue.Peek();
                    Console.WriteLine($"Removing {elementOnFront}...");
                    var removedElement = myNodeQueue.DeQueue();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                // adding random values to the queue
                var newElement = random.Next(RANGE_VALUES);
                Console.WriteLine($"Inserting {newElement}");
                myNodeQueue.Enqueue(newElement);
            }
            Console.WriteLine(myNodeQueue);
        }

    }
}
