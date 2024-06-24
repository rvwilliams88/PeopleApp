namespace PeopleApp;
using Library;

public partial record Person(string? FirstName, string? LastName, string? FullName);

public interface IPeopleService
{
    ValueTask<IImmutableList<Person>> GetPeople(CancellationToken ct);
}

public class PeopleService : IPeopleService
{
    AuthorService? AuthorService { get; set; }
    LibraryFunctions? LibraryFunctions { get; set; }
    LibraryService? LibraryService { get; set; }
    public async ValueTask<IImmutableList<Person>> GetPeople(CancellationToken ct)
    {
        AuthorService = new AuthorService();
        LibraryFunctions = new LibraryFunctions();
        LibraryService = new LibraryService();
        AuthorService.authorsCollection = LibraryFunctions.authorsCollection;

        List<Author>? authors = new List<Author>();
        authors = await AuthorService.getAuthors();

        List<Person> peopleList = new List<Person>();
        if (authors == null || authors.Count == 0)
        {
            return ImmutableList<Person>.Empty;
        }
        foreach (var author in authors.Take(10))
        {
            peopleList.Add(new Person(FirstName: author.first, LastName: author.last, FullName: author.FullName));
        }
        var people = peopleList.ToArray();

        return people.ToImmutableList();
    }
}


