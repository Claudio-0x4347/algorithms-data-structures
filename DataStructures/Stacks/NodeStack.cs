using System.Text;

namespace DataStructures.Stacks;
/*
	-----------------------------
    | operation			| cost	|	
    -----------------------------
	| Push			    | T(1)	|
	| Pos			    | T(1)	|
	| Peek				| T(1)	|
	-----------------------------
	T(n) -> Theta(n)
	O(n) -> O(n)
	I will not consider best case for simplicity.
*/
public class NodeStack<T> : IStack<T>
{
    private class Node
    {
        public T Element { get; set; }
        public Node Previous { get; set; }
        public Node(T element, Node previous)
        {
            Element = element;
            Previous = previous;
        }
    }

    private Node Top { get; set; } = null;
    public int Count { get; set; } = 0;

    // Check element on top cost T(1) - indexing
    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException();

        return Top.Element;
    }

    // Remove element on top cost T(1)
    public T Pop()
    {
        if(Count == 0 )
            throw new InvalidOperationException();

        Node removedNode = Top;
        Top = removedNode.Previous;
        Count--;
        return removedNode.Element;
    }

    // Insert new element on stack is T(1)
    public void Push(T element)
    {
        Node newNode = new Node(element, Top);
        Top = newNode;
        Count++;
    }

    public override string ToString()
    {
        var output = new StringBuilder(nameof(NodeStack<T>));
        output.Append("([");
        Node current = Top;
        while(current != null && current.Previous != null)
        {
            output.Append($"{current.Element}, ");
            current = current.Previous;
        }
        if(current != null)
            output.Append(current.Element);
        output.Append($"], count: {Count})");
        return output.ToString();
    }
}
