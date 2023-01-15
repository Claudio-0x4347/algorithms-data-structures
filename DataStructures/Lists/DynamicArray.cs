using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Lists;

/*
	-------------------------------------------------
    | operation			| first	| arbitrary	| last	|	
    -------------------------------------------------
	| get				| T(1)	|	T(1)	| T(1)	|
	| set				| T(1)	|	T(1)	| T(1)	|
	| Add				| ----	|	----	| O(n)	|
	| InsertAt			| O(n)	|	O(n)	| O(n)	|
	| Remove			| ----	|	O(n)	| ----	|
	| RemoveAll			| ----	|	T(n)	| ----	|
	| RemoveAt			| O(n)	|	O(n)	| T(1)	|
	| IndexOf			| ----	|	O(n)	| ----	|
	| Contains			| ----	|	O(n)	| ----	|
	| Clear				| ----	|	T(1)	| ----	|
	-------------------------------------------------
	T(n) -> Theta(n)
	O(n) -> O(n)
	I will not consider best case for simplicity.
*/
public class DynamicArray<T> : IList<T> where T : IComparable
{
    public static readonly int DEFAULT_INITIAL_CAPACITY = 4;
    private T[] _array;
    public int Capacity { get; private set; }
    public int Length { get; private set; }

    // initialize with capacity
    public DynamicArray(int capacity)
    {
        if (capacity <= 0)
            throw new System.ArgumentException($"{nameof(capacity)} must be greater or equal to 0.");

        Capacity = capacity;
        _array = new T[Capacity];
        Length = 0;
    }

    // default constructor
    public DynamicArray() : this(DEFAULT_INITIAL_CAPACITY)
    { }

    // initialize from a vector takes Theta(n)
    public DynamicArray(T[] array)
    {
        Capacity = 2 * array.Length;
        _array = new T[Capacity];

        // copy values and update Length
        for (int i = 0; i < array.Length; i++)
            _array[i] = array[i];
        Length = array.Length;
    }

    // indexing cost T(1) since we implemented with array
    public T this[int index]
    {
        get
        {
            IndexInBoundsOrThrow(index);
            return this._array[index];
        }
        set
        {
            IndexInBoundsOrThrow(index);
            this._array[index] = value;
        }
    }

    // Insert element at the end of the array and takes O(n)
    // if we need to update the size of array
    public void Add(T value)
    {
        // check for update array size
        if (Length >= Capacity)
        {
            int newCapacity = 2 * Capacity;
            var newArray = new T[newCapacity];

            // copy values to new array
            for (int i = 0; i < Capacity; i++)
                newArray[i] = _array[i];
            Capacity = newCapacity;

            // free old array memory
            _array = newArray;
        }

        // add element and update Length
        _array[Length++] = value;
    }

    // Insert element at index takes O(n) since we
    // need to shift elements from array and eventualy 
	// alloc memory
    public void InsertAt(int index, T element)
    {
        if(index == 0 && Length == 0)
        {
            _array[0] = element;
            Length++;
        }
        else
        {
            IndexInBoundsOrThrow(index);

            // check for update array size
            if (Length >= Capacity)
            {
                int newCapacity = 2 * Capacity;
                var newArray = new T[newCapacity];

                // copy values ultil index
                for (int i = 0; i < index; i++)
                    newArray[i] = _array[i];

                // shift other elements
                for (int i = index; i < Capacity; i++)
                    newArray[i + 1] = _array[i];

                // insert new element and update capacity and length
                newArray[index] = element;
                Capacity = newCapacity;
                Length++;

                // free old array memory
                _array = newArray;
            }
            else
            {
                // right shift elements after index
                for (int i = Length; i > index; i--)
                    _array[i] = _array[i - 1];

                // insert new element and update length
                _array[index] = element;
                Length++;
            }
        }
    }

    // Remove the first occurence of an element takes O(n) - linear search
    public void Remove(T element)
    {
        // find element position
        int indexElement = IndexOf(element);

        // update length and shift elements to left
        if (indexElement < Length && indexElement > 0)
        {
            Length--;
            for (int i = indexElement; i < Length; i++)
                _array[i] = _array[i + 1];

            // update state
        }
    }

	// remove all instance takes T(n) since we need to check all elements
    public void RemoveAll(T element)
    {
        // initialize pointers
        int indexCopy = 0;
        int indexElement = 0;
        for (; indexElement < Length; indexElement++)
        {
            // skip elements with value passed
            if (_array[indexElement].Equals(element))
                continue;

            // copy element and update pointer
            _array[indexCopy++] = _array[indexElement];
        }

        // update the length based on the last element copied
        Length = indexCopy;
    }

	// remove at position cost T(1) if index is the last element
	// otherwise cost O(n) since we need to copy right subarray
    public void RemoveAt(int index)
    {
        IndexInBoundsOrThrow(index);

        // update length and left shift elements
        Length--;
        for (int i = index; i < Length; i++)
        {
            _array[i] = _array[i + 1];
        }
    }

    // find element takes O(n) - linear search
    public int IndexOf(T element)
    {
        for (int i = 0; i < Length; i++)
            if (_array[i].Equals(element))
                return i;
        return -1;
    }

    public bool Contains(T element)
    {
        return IndexOf(element) >= 0;
    }

    // clear takes Theta(1) sice list was implemented with array
    public void Clear()
    {
        Capacity = DEFAULT_INITIAL_CAPACITY;
        _array = new T[Capacity];   // old array elegible for garbage collector
        Length = 0;
    }

    public override string ToString()
    {
        var output = new StringBuilder($"DynamicArray([");
        if (Length > 0)
            output.Append(_array[0]);
        for (int i = 1; i < Length; i++)
            output.AppendFormat(", {0}", _array[i]);
        output.AppendFormat("], len: {0}, cap: {1})", Length, Capacity);
        return output.ToString();
    }

    private void IndexInBoundsOrThrow(int index)
    {
        if (index >= Length || index < 0)
            throw new IndexOutOfRangeException();
    }

}
