using System.Data;

namespace BookManagement_E19;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Isbn { get; set; }
    public string? BorrowedBy {  get; private set; }
    public DateTime? DueDate { get; private set; }
    public List<(string reader, DateTime start, DateTime end)> History { get; private set; } = [];

    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        Isbn = isbn;
    }

    public void Borrow(string readerId)
    {
        if (BorrowedBy != null)
        {
            Console.WriteLine("Invalid operation. Book aleardy borrowed.");
            return;
        }
        BorrowedBy = readerId;
        DueDate = DateTime.Now.AddDays(7);
        History.Add((readerId, DateTime.Now, DateTime.Now.AddDays(7)));
    }

    public void Return()
    {
        var historyRec = History.FirstOrDefault(r => r.reader == BorrowedBy && r.end == DueDate);
        historyRec.end = DateTime.Now; 
        BorrowedBy = null;
        DueDate = null;
    }

    public override string ToString()
    {
        var status = BorrowedBy == null ? "Available." : $"Borrowed by {BorrowedBy}.";
        return $"Author: {Author}\nTitle: {Title}\nIsbn: ({Isbn})\nStatus: {status}\n";
    }
}
