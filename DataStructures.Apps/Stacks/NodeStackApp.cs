using DataStructures.Stacks;

namespace DataStructures.Apps.Stacks;

public static class NodeStackApp
{
    private static readonly int RANGE_VALUES = 10;
    private static readonly int NUMBER_INPUTS = 10;
    public static void Run()
    {
        // initialize an empty stack
        var myArrayStack = new NodeStack<string>();

        Console.Write("Empty Stack: ");
        Console.WriteLine(myArrayStack);

        // adding random values to stack
        Random random = new Random();
        for (int i = 0; i < NUMBER_INPUTS; i++)
        {
            var newElement = random.Next(RANGE_VALUES);
            myArrayStack.Push(newElement.ToString());
            Console.WriteLine($"Inserting {newElement}");
            Console.WriteLine(myArrayStack);
        }

        // removing values from stack
        try
        {
            while (true)
            {
                var elementOnTop = myArrayStack.Peek();
                Console.WriteLine($"Removing {elementOnTop}...");
                var removedElement = myArrayStack.Pop();
                Console.WriteLine($"After remove {removedElement}: {myArrayStack}");
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(myArrayStack);
        }
    }
}
