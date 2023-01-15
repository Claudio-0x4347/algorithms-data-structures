using System.Text;

namespace DataStructures.Lists;

/*
	-------------------------------------------------
    | operation			| first	| arbitrary	| last	|	
    -------------------------------------------------
	| get				| T(1)	|	O(n)*	| T(1)	|
	| set				| T(1)	|	O(n)*	| T(1)	|
	| Add				| ----	|	----	| T(1)	|
	| InsertAt			| T(1)	|	O(n)*	| T(1)*	|
	| Remove			| ----	|	O(n)	| ----	|
	| RemoveAll			| ----	|	T(n)	| ----	|
	| RemoveAt			| T(1)	|	O(n)*	| T(1)*	|
	| IndexOf			| ----	|	O(n)	| ----	|
	| Contains			| ----	|	O(n)	| ----	|
	| Clear				| ----	|	T(n)	| ----	|
	-------------------------------------------------
	T(n) -> Theta(n)
	O(n) -> O(n)
	I will not consider best case for simplicity.

    * is the gain over singly linked list. Because asymptotic notation
    does not consider the gain in decision between traverse forward/backward 
    depending on index, some operation appear with same notation
*/
public class DoublyLinkedList<T> : IList<T> where T : IComparable
{
    private class Node
    {
        public T Element { get; set; }
        public Node Next { get; set; }
        public Node Previous { get; set; }
        public Node(T element, Node next, Node previous)
        {
            Element = element;
            Next = next;
            Previous = previous;
        }
    }

    private Node Head { get; set; }
    private Node Tail { get; set; }
    public int Count { get; private set; }

    public DoublyLinkedList()
    {
        Head = null;
        Tail = null;
        Count = 0;
    }

    // indexing takes O(n)* - linear search
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
            Head = Tail = new Node(element, null, null);
        else
            Tail = Tail.Next = new Node(element, null, Tail);
        Count++;
    }

    // inserting in index cost T(1) to first and last index
    // otherwise perform in O(n) because of traverse
    public void InsertAt(int index, T element)
    {    
        if(Count == 0)
        {
            Head = Tail = new Node(element, null, null);
        }
        else
        {
            Node oldNode = TraverseTo(index);
            Node newNode = new Node(element, oldNode,  oldNode.Previous);
            oldNode.Previous = newNode;

            if(index == 0)
                Head = newNode;
            else
            {
                if(index == (Count-1))
                    Tail = newNode;
            }
        }
        Count++;
    }

    // removing an element cost O(n) - linear search
    public void Remove(T element)
    {
        if (Count > 0)
        {
            // traverse to element
            Node current = Head;
            Node previous = current;
            while (current != null && !current.Element.Equals(element))
            {
                previous = current;
                current = current.Next;
            }

            // remove element in head and middle/tail and update
            // tail pointer
            if (current == Head)
            {
                Head = current.Next;
                Tail = current == Tail ? null : Tail;
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
                    previous.Next = current.Next;
                    current.Next = null;    // available to gc

                    current = previous.Next;
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

    // remove the first/last element cost T(1) but an arbitrary
    // cost O(n) because we need to traverse to one index before it
    public void RemoveAt(int index)
    {
        IndexInBoundsOrThrow(index);

        Node removedNode = TraverseTo(index);
        if(Count == 1)
        {
            Head = Tail = null;     // available to gc
        }
        else if(removedNode == Head)
        {
            removedNode.Next.Previous = null;
            Head = removedNode.Next;
        }
        else if(removedNode == Tail)
        {
            removedNode.Previous.Next = null;
            Tail = removedNode.Previous;
        }
        else
        {
            removedNode.Previous.Next = removedNode.Next;
            removedNode.Next.Previous = removedNode.Previous;
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
                if(current.Element.Equals(element))
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

        // choose between forward/backward
        int distanceForward = index;
        int distanceBackward = Count - index;

        if(distanceForward < distanceBackward)
        {
            Node current = Head;
            for (int i = 0; i < distanceForward; i++)
                current = current.Next;
            return current;
        }
        else
        {
            Node current = Tail;
            for (int i = 0; i < distanceBackward; i++)
                current = current.Previous;
            return current;
        }
    }
}