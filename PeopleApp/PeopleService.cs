namespace PeopleApp;

public partial record Person(string? FirstName, string? LastName, string? FullName);

public interface IPeopleService
{
    ValueTask<IImmutableList<Person>> GetPeopleAsync(CancellationToken ct);
}

public class PeopleService : IPeopleService
{
    public async ValueTask<IImmutableList<Person>> GetPeopleAsync(CancellationToken ct)
    {
        await Task.Delay(TimeSpan.FromSeconds(2), ct);

        var people = new Person[]
        {
            new Person(FirstName: "Peter", LastName: "Ackroyd", FullName: "Peter Ackroyd"),
            new Person(FirstName: "Jane", LastName: "Adams", FullName: "Jane Adams")
        };

        return people.ToImmutableList();
    }
}


