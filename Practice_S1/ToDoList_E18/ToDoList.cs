namespace ToDoList_E18;

public class ToDoList
{
    private readonly List<Item> _items = [];

    public void AddTask(Item item) => _items.Add(item);
    public void RemoveTask(Guid id) => _items.RemoveAll(i => i.Id == id);

    public IEnumerable<Item> GetTasks(bool? completed = null)
    {
        if(completed == null) return _items;
        return _items.Where(i => i.IsCompleted == completed);
    }
    public void Display()
    {
        foreach(var item in _items)
            Console.WriteLine(item);
    }
}
