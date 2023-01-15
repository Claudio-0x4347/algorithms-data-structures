using System.Text;

namespace DataStructures.Stacks;

/*
	-----------------------------
    | operation			| cost	|	
    -----------------------------
	| Push			    | O(n)	|
	| Pop			    | T(1)	|
	| Peek				| T(1)	|
	-----------------------------
	T(n) -> Theta(n)
	O(n) -> O(n)
	I will not consider best case for simplicity.
*/
public class ArrayStack<T> : IStack<T>
{
    private static int DEFAULT_INITIAL_SIZE = 4;
    private T[] _array;
    public int Capacity { get; private set; }
    public int Count { get; private set; }

    public ArrayStack(int size)
    {
        _array = new T[DEFAULT_INITIAL_SIZE];
        Capacity = size;
        Count = 0;
    }

    public ArrayStack() : this(DEFAULT_INITIAL_SIZE)
    { }

    // Checking element on top cost T(1) - indexing
    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException();
        return _array[Count - 1];
    }

    // Remove the element on top cost T(1)
    public T Pop()
    {
        if (Count == 0)
            throw new InvalidOperationException();
        return _array[--Count];
    }

    // Insert element cost O(n) since we need to copy elements
    // when allocating memory
    public void Push(T element)
    {
        if ((Count + 1) == Capacity)
        {
            var newCapacity = 2 * Capacity;
            var newArray = new T[newCapacity];
            for (int i = 0; i < Capacity; i++)
                newArray[i] = _array[i];
            _array = newArray;
            Capacity = newCapacity;
        }
        _array[Count++] = element;
    }

    public override string ToString()
    {
        var output = new StringBuilder(nameof(ArrayStack<T>));
        output.Append("([");
        var preCount = Count - 1;
        for (int i = preCount; i >= 1; i--)
        {
            output.Append(_array[i]);
            output.Append(", ");
        }
        if(Count>0)
            output.Append(_array[0]);
        output.Append($"], count: {Count}, capacity: {Capacity})");
        return output.ToString();
    }
}