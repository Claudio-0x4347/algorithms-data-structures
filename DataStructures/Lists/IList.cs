namespace DataStructures.Lists;

public interface IList<T> where T : IComparable
{
    // get and update element in position
    T this[int index] { get; set; }

    // Insert at the last position
    void Add(T element);
    
    // Insert at arbitrary positon
    void InsertAt(int index, T element);
    
    // Remove the first instance of an element
    void Remove(T element);
    
    // Remove the all instance of an element
    void RemoveAll(T element);
    
    // Remove in position
    void RemoveAt(int index);
    
    // Check the position of first instance
    int IndexOf(T element);
    
    // Check if an element is present
    bool Contains(T element);
    
    // empty the list and free some memory
    void Clear();
}
