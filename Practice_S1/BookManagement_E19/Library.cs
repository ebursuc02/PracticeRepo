namespace BookManagement_E19
{
    public class Library
    {
        private readonly List<Book> _books = [];
        public void AddBook(Book book) => _books.Add(book);
        public void RemoveBook(string isbn) => _books.RemoveAll(b => b.Isbn == isbn);
        public Book? FindBookByTitle(string title) => _books.FirstOrDefault(b => b.Title == title);
        public IEnumerable<Book> FindBooksByAuthor(string author) => _books.Where(b => b.Author == author);
        public void Display()
        {
            foreach (var book in _books)
                Console.WriteLine(book);
        }
    }
}
