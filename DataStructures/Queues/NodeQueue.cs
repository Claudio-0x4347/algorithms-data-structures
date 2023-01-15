using System.Text;

namespace DataStructures.Queues;

/*
	-----------------------------
    | operation			| cost	|	
    -----------------------------
	| Enqueue			| T(1)	|
	| Dequeue			| T(1)	|
	| Peek				| T(1)	|
	-----------------------------
	T(n) -> Theta(n)
	O(n) -> O(n)
	I will not consider best case for simplicity.
*/
public class NodeQueue<T> : IQueue<T>
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
    public int Length { get; private set; }

    // Removing an element cost T(1), just state updates and memory release
    public T DeQueue()
    {
        if (Length <= 0)
            throw new InvalidOperationException();
        Node removedNode = Head;
        Head = Head.Next;
        Length--;
        return removedNode.Element; // removedNode elegible to gc
    }

    // Insert an element cost T(1) because we have pointer to tail
    public void Enqueue(T element)
    {
        if (Length <= 0)
            Head = Tail = new Node(element, null);
        else
        {
            Node newNode = new Node(element, null);
            Tail.Next = newNode;
            Tail = newNode;
        }
        Length++;
    }

    // Checking first element cost T(1)
    public T Peek()
    {
        if (Length <= 0)
            throw new InvalidOperationException();
        return Head.Element;
    }

    public override string ToString()
    {
        var output = new StringBuilder(nameof(NodeQueue<T>));
        output.Append("([");
        Node current = Head;
        while(current!=null)
        {
            if(current.Next == null)
                output.Append($"{current.Element}");
            else
                output.Append($"{current.Element}, ");
            current = current.Next;
        }
        output.Append($"], len: {Length})");
        return output.ToString();
    }
}