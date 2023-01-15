namespace DataStructures.Stacks;

public interface IStack<T>
{
    // Insert element on top of stack
    void Push(T element);

    // Remove element on top of the stack
    T Pop();

    // Check element on top without remove it
    T Peek();
}
