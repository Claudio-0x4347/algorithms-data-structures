using System.Text;

namespace DataStructures.Lists;

/*
	-------------------------------------------------
    | operation			| first	| arbitrary	| last	|	
    -------------------------------------------------
	| get				| T(1)	|	O(n)	| T(1)	|
	| set				| T(1)	|	O(n)	| T(1)	|
	| Add				| ----	|	----	| T(1)	|
	| InsertAt			| T(1)	|	O(n)	| T(1)	|
	| Remove			| ----	|	O(n)	| ----	|
	| RemoveAll			| ----	|	T(n)	| ----	|
	| RemoveAt			| T(1)	|	O(n)	| T(n)	|
	| IndexOf			| ----	|	O(n)	| ----	|
	| Contains			| ----	|	O(n)	| ----	|
	| Clear				| ----	|	T(n)	| ----	|
	-------------------------------------------------
	T(n) -> Theta(n)
	O(n) -> O(n)
	I will not consider best case for simplicity.
*/
public class SinglyLinkedList<T> : IList<T> where T : IComparable
{
    private class Node
    {
        public T Element { get; set; }
        public Node Next { get; set; }
        public Node(T element, Node next)
        {
            Element = element;
            Next = next;
        }
    }

    private Node Head { get; set; }
    private Node Tail { get; set; }
    public int Count { get; private set; }

    public SinglyLinkedList()
    {
        Head = null;
        Tail = null;
        Count = 0;
    }

    // indexing takes O(n) - linear search
    public T this[int index]
    {
        get
        {
            return TraverseTo(index).Element;
        }
        set
        {
            TraverseTo(index).Element = value;
        }
    }

    // inserting an element in linked list cost T(1)
    public void Add(T element)
    {
        if (Head == null)
            Head = Tail = new Node(element, null);
        else
            Tail = Tail.Next = new Node(element, null);
        Count++;
    }

    // inserting in index cost T(1) to first and last index
    // otherwise perform in O(n) because of linear search
    public void InsertAt(int index, T element)
    {
        if (Count == 0 && index == 0)
        {
            Head = Tail = new Node(element, null);
        }
        else
        {
            IndexInBoundsOrThrow(index);

            if (index == 0)
            {
                Node newNode = new Node(element, Head);
                Head = newNode;
            }
            else if (index == (Count - 1))
            {
                Node newNode = new Node(element, null);
                Tail.Next = newNode;
                Tail = newNode;
            }
            else
            {
                Node current = Head;
                for (int i = 1/*stop 1 early*/; i < index; i++)
                    current = current.Next;
                current.Next = new Node(element, current.Next);
            }
        }
        Count++;
    }

    // removing an element cost O(n) - linear search for pre-searched element
    public void Remove(T element)
    {
        if (Count > 0)
        {
            // traverse to element
            Node current = Head;
            Node previous = current;
            while (current != null && current.Next != null)
            {
                if (current.Element.Equals(element))
                    break;
                previous = current;
                current = current.Next;
            }

            // element found
            if (current != null)
            {
                if (current == Head)
                {
                    Head = current.Next;
                    Tail = current == Tail ? null : Tail;
                }
                else if (current == Tail)
                {
                    Tail = previous;
                    Tail.Next = null;
                }
                else
                {
                    previous.Next = current.Next;
                    Tail = current == Tail ? previous : Tail;
                }

                // update state and free memory
                Count--;
            }
        }
    }

    // perform linear search and remove all occurrence cost T(n)
    public void RemoveAll(T element)
    {
        if (Count == 1)
        {
            if (Head.Element.Equals(element))
            {
                Head = Tail = null;
                Count = 0;
            }
        }
        else
        {
            Node current = Head;
            Node previous = current;
            while (current != null)
            {
                if (current.Element.Equals(element))
                {
                    if (current != Head)
                    {
                        previous.Next = current.Next;
                        current = previous.Next;
                    }
                    else
                    {
                        Head = Head.Next;
                        current = Head;
                        previous = current;
                    }
                    Count--;
                }
                else
                {
                    previous = current;
                    current = current.Next;
                }

            }
        }
    }

    // remove the first element cost T(1) but an arbitrary
    // cost O(n) because we need to traverse to one index before
    // and particulaly the last cost T(n) since we can't find the
    // previous index with next pointer in singly linked list
    public void RemoveAt(int index)
    {
        if (Count > 0 && index == 0)
        {
            Tail = Tail == Head ? null : Tail;
            Head = Head.Next;
        }
        else
        {
            IndexInBoundsOrThrow(index);

            // traverse to node immediately before the index and
            // remove the node in index
            Node previous = TraverseTo(index - 1);
            Tail = Tail == previous.Next ? previous : previous.Next;
            previous.Next = previous.Next.Next;
        }

        // update state and free memory
        Count--;
    }

    // search an element cost O(n) - linear search
    public int IndexOf(T element)
    {
        if (Count > 0)
        {
            // traverse to first occurrence
            int index = 0;
            Node current = Head;
            while (current != null)
            {
                if (current.Element.Equals(element))
                    break;
                current = current.Next;
                index++;
            }

            // check if element was found
            if (current != null)
                return index;
        }
        return -1;
    }
    public bool Contains(T element)
    {
        return IndexOf(element) > 0;
    }

    // clear one node each time cost T(n)
    public void Clear()
    {
        // remove nodes one at time from head
        while (Count > 0)
            RemoveAt(0);
    }

    public override string ToString()
    {
        StringBuilder output = new StringBuilder($"{nameof(SinglyLinkedList<T>)}([");
        Node current = Head;
        while (current != null)
        {
            output.Append($"{current.Element}, ");
            current = current.Next;
        }
        output.Append($"null], count: {Count})");
        return output.ToString();
    }

    private void IndexInBoundsOrThrow(int index)
    {
        if (index >= Count || index < 0)
            throw new IndexOutOfRangeException();
    }

    private Node TraverseTo(int index)
    {
        IndexInBoundsOrThrow(index);

        Node current = Head;
        for (int i = 0; i < index; i++)
            current = current.Next;
        return current;
    }
}