using System.Text;

namespace DataStructures.Queues;

/*
	-----------------------------
    | operation			| cost	|	
    -----------------------------
	| Enqueue			| O(n)	|
	| Dequeue			| T(1)	|
	| Peek				| T(1)	|
	-----------------------------
	T(n) -> Theta(n)
	O(n) -> O(n)
	I will not consider best case for simplicity.
*/
public class ArrayQueue<T> : IQueue<T>
{
    public static readonly int DEFAULT_INITIAL_CAPACITY = 4;
    private T[] _array;
    public int Capacity { get; private set; }

    private int Head { get; set; }
    private int Tail { get; set; }
    public int Length
    {
        get
        {
            if (Tail >= Head)
                return Tail - Head;
            return Capacity - Head + Tail;
        }
    }

    public ArrayQueue(int capacity)
    {
        Capacity = capacity;
        _array = new T[Capacity];
        Head = Tail = 0;
    }

    public ArrayQueue() : this(DEFAULT_INITIAL_CAPACITY)
    { }

    // Remove an element from queue cost T(1) since we only need update Head pointer
    public T DeQueue()
    {
        if (Length <= 0)
            throw new InvalidOperationException();

        var firstElement = _array[Head];
        Head = (Head + 1) % Capacity;       // circular updating
        return firstElement;
    }

    // Insert an element on the queue cost O(n) because we need
    // to update array size sometimes and this require copy elements
    public void Enqueue(T element)
    {
        if ((Length + 1) >= Capacity)
        {
            // allocate memory and copy to new array
            var newCapacity = 2 * Capacity;
            var newArray = new T[newCapacity];
            int i = Head;
            int j = 0;
            for (; j < Length; j++)
            {
                newArray[j] = _array[i];
                i = (i + 1) % Capacity;
            }
            _array = newArray;  // old array elegible to gc
            Capacity = newCapacity;
            Head = 0;
            Tail = j;
        }

        // append to the queue and update length
        _array[Tail] = element;
        Tail = (Tail + 1) % Capacity;   // circular updating
    }

    // Check the first element without removing cost T(1) - indexing
    public T Peek()
    {
        if (Length <= 0)
            throw new InvalidOperationException();
        return _array[Head];
    }

    public override string ToString()
    {
        var output = new StringBuilder(nameof(ArrayQueue<T>));
        output.Append("([");
        if(Length > 0 )
        {
            int current = Head;
            for (int i = 0; i < (Length - 1); i++)
            {
                output.Append($"{_array[current]}, ");
                current = (current + 1) % Capacity;     // circular update
            }
            output.Append(_array[current]);
        }
        output.Append($"], len: {Length}, cap: {Capacity})");
        return output.ToString();
    }
}