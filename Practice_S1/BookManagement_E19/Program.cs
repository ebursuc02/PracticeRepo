using BookManagement_E19;

List<Book> books = new List<Book>
{
    new Book("Clean Code: A Handbook of Agile Software Craftsmanship",
             "Robert C. Martin",
             "978-0132350884"),
    new Book("The Pragmatic Programmer: Your Journey to Mastery (20th Anniversary Edition)",
             "Andrew Hunt, David Thomas",
             "978-0135957059"),
    new Book("Design Patterns: Elements of Reusable Object-Oriented Software",
             "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides",
             "978-0201633610"),
    new Book("You Don’t Know JS Yet: Get Started",
             "Kyle Simpson",
             "978-1091210099")
};

Library library = new Library();

foreach(Book book in books)
    library.AddBook(book);

library.Display();

// some actions with book objects
books[0].Borrow("R_989821");
books[2].Borrow("R_678103");

// try to borrow by another reader
books[2].Borrow("R_989821");
Console.WriteLine();

library.Display();