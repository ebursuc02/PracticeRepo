namespace ToDoList_E18;

public class Item
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Content { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Item(string content) => Content = content;

    public void MarkCompleted() => IsCompleted = true;
    public override string ToString()
    {
        char completedChar = IsCompleted ? 'X' : 'O';
        return $"{completedChar} {Content}";
    }
}
