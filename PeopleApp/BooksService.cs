namespace PeopleApp;

public partial record BookRead(string? Title);

public interface IBooksService
{
    ValueTask<IImmutableList<BookRead>> GetBooksRead(Person selectedPerson, CancellationToken ct);
}

public class BooksService : IBooksService
{
    public async ValueTask<IImmutableList<BookRead>> GetBooksRead(Person selectedPerson, CancellationToken ct)
    {
        List<BookRead> books = new List<BookRead>();
        await Task.Delay(TimeSpan.FromSeconds(2), ct);
        if (selectedPerson.FullName != "Jane Adams")
        {
            books.Add(new BookRead(Title: "not Jane Adams"));
        }
        else
        {
            books.Add(new BookRead(Title: "My favourite title"));
            books.Add(new BookRead(Title: "another title"));
            books.Add(new BookRead(Title: "One more title"));
        }
        var booksRead = books.ToArray();
        return booksRead.ToImmutableList();
    }
}
