using DataStructures.Lists;

namespace DataStructures.Apps.Lists;

public static class SinglyLinkedListApp
{
    private static readonly int RANGE_VALUES = 10;
    private static readonly int NUMBER_INPUTS = 25;
    public static void Run()
    {
        /************************************************************/
        /************************* Element **************************/
        /************************************************************/
        var myLinkedList = new SinglyLinkedList<string>();
        Random random = new Random();
        Console.WriteLine($"Empty array: {myLinkedList}");

        // Testing Add()
        for (int i = 0; i < NUMBER_INPUTS; i++)
        {
            myLinkedList.Add(random.Next(RANGE_VALUES).ToString());
        }
        Console.WriteLine($"\nAfter populating array");
        Console.WriteLine(myLinkedList);

        // Testing Remove()
        for (int i = 0; i < 3; i++)
        {
            string removedValue = random.Next(RANGE_VALUES).ToString();
            myLinkedList.Remove(removedValue);
            Console.WriteLine($"\nAfter Remove({removedValue})");
            Console.WriteLine(myLinkedList);
        }

        // Testing RemoveAll()
        string middleValue = (RANGE_VALUES / 2).ToString();
        myLinkedList.RemoveAll(middleValue);
        Console.WriteLine($"\nAfter RemoveAll({middleValue})");
        Console.WriteLine(myLinkedList);

        // Testing Clear()
        myLinkedList.Clear();

        /************************************************************/
        /************************* Index ****************************/
        /************************************************************/

        // Testing InsertAt()
        for (int i = 0; i < NUMBER_INPUTS; i++)
        {
            int position = random.Next(NUMBER_INPUTS);
            string value = random.Next(RANGE_VALUES).ToString();
            try
            {
                myLinkedList.InsertAt(position, value);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Failed to insert {value} at position {position}. Inserting in beginning...");
                myLinkedList.InsertAt(0, value);
            }
        }
        Console.WriteLine("\nAfter populating array");
        Console.WriteLine(myLinkedList);

        // Testing RemoveAt()
        int firstIndex = 0;
        Console.WriteLine($"\nAfter RemoveAt({nameof(firstIndex)}): {myLinkedList[firstIndex]}");
        myLinkedList.RemoveAt(0);
        Console.WriteLine(myLinkedList);

        int middleIndex = myLinkedList.Count / 2;
        Console.WriteLine($"\nAfter Remove({nameof(middleIndex)}): {myLinkedList[middleIndex]}");
        myLinkedList.RemoveAt(middleIndex);
        Console.WriteLine(myLinkedList);

        int lastIndex = myLinkedList.Count-1;
        Console.WriteLine($"\nAfter Remove({nameof(lastIndex)}): {myLinkedList[lastIndex]}");
        myLinkedList.RemoveAt(lastIndex);
        Console.WriteLine(myLinkedList);

        // Testing set, IndexOf and Contains*
        while (myLinkedList.Contains(middleValue))
        {
            int position = myLinkedList.IndexOf(middleValue);
            myLinkedList[position] = "-1";
        }
        Console.WriteLine($"After replaced {middleValue} to -1");
        Console.WriteLine(myLinkedList);
        Console.WriteLine($"Contains({middleValue}): {myLinkedList.Contains(middleValue)}");
    }
}