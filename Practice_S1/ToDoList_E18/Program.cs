using ToDoList_E18;

ToDoList list = new ToDoList();

List<Item> items = new List<Item>()
{
    new Item("Go to the grocery shop"),
    new Item("Clean the house"),
    new Item("Do the homework")
};

foreach(Item item in items)
    list.AddTask(item);

list.Display();
Console.WriteLine();

// some actions on item and list objects
items[1].MarkCompleted();
list.RemoveTask(items[2].Id);

list.Display();
