using DataStructures.Lists;

namespace DataStructures.Apps.Lists;

public static class DynamicArrayApp
{
    private static readonly int RANGE_VALUES = 10;
    private static readonly int NUMBER_INPUTS = 25;
    public static void Run()
    {
        // instantiate from a vector
        var myArray = new DynamicArray<int>(new[] { 1, 2, 3, 4, 5 });
        Console.WriteLine($"From vector: {myArray}");

        /************************************************************/
        /************************* Element **************************/
        /************************************************************/
        myArray = new DynamicArray<int>();
        Random random = new Random();
        Console.WriteLine($"Empty array: {myArray}");

        // Testing Add()
        for (int i = 0; i < NUMBER_INPUTS; i++)
        {
            myArray.Add(random.Next(RANGE_VALUES));
        }
        Console.WriteLine($"\nAfter populating array");
        Console.WriteLine(myArray);

        // Testing Remove()
        for (int i = 0; i < 3; i++)
        {
            int removedValue = random.Next(RANGE_VALUES);
            myArray.Remove(removedValue);
            Console.WriteLine($"\nAfter Remove({removedValue})");
            Console.WriteLine(myArray);
        }

        // Testing RemoveAll()
        int middleValue = RANGE_VALUES / 2;
        myArray.RemoveAll(middleValue);
        Console.WriteLine($"\nAfter RemoveAll({middleValue})");
        Console.WriteLine(myArray);

        // Testing Clear()
        myArray.Clear();

        /************************************************************/
        /************************* Index ****************************/
        /************************************************************/

        // Testing InsertAt()
        for (int i = 0; i < NUMBER_INPUTS; i++)
        {
            int position = random.Next(NUMBER_INPUTS);
            int value = random.Next(RANGE_VALUES);
            try
            {
                myArray.InsertAt(position, value);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Failed to insert {value} at position {position}. Inserting in beginning...");
                myArray.InsertAt(0, value);
            }
        }
        Console.WriteLine($"\nAfter populating array");
        Console.WriteLine(myArray);

        // Testing RemoveAt()
        int firstIndex = 0;
        Console.WriteLine($"\nAfter RemoveAt({nameof(firstIndex)}): {myArray[firstIndex]}");
        myArray.RemoveAt(0);
        Console.WriteLine(myArray);

        int middleIndex = myArray.Length / 2;
        Console.WriteLine($"\nAfter Remove({nameof(middleIndex)}): {myArray[middleIndex]}");
        myArray.RemoveAt(middleIndex);
        Console.WriteLine(myArray);

        int lastIndex = myArray.Length-1;
        Console.WriteLine($"\nAfter Remove({nameof(lastIndex)}): {myArray[lastIndex]}");
        myArray.RemoveAt(lastIndex);
        Console.WriteLine(myArray);

        // Testing set, IndexOf and Contains*
        while (myArray.Contains(middleValue))
        {
            int position = myArray.IndexOf(middleValue);
            myArray[position] = -1;
        }
        Console.WriteLine($"After replaced {middleValue} to -1");
        Console.WriteLine(myArray);
        Console.WriteLine($"Contains({middleValue}): {myArray.Contains(middleValue)}");
    }
}