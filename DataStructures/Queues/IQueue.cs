namespace DataStructures.Queues;

public interface IQueue<T>
{
    // Insert element on back of Queue
    void Enqueue(T element);

    // Remove element on front of Queue
    T DeQueue();

    // Check element on front without remove it
    T Peek();
}