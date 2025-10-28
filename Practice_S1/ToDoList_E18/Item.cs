namespace ToDoList_E18;

public class Item
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Content { get; private set; } = string.Empty;
    public bool IsCompleted { get; private set; } = false;
    public DateTime CreatedAt { get; } = DateTime.Now;

    public Item(string content) => Content = content;

    public void MarkCompleted() => IsCompleted = true;

    public void EditContent(string content) => Content = content;

    public override string ToString()
    {
        char completedChar = IsCompleted ? 'X' : 'O';
        return $"{completedChar} {Content}";
    }
}
