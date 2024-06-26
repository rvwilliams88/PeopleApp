namespace PeopleApp;

public partial record PeopleModel
{
    private IPeopleService _peopleService;
    private IBooksService _booksService;
    public CancellationToken? _ct;
    public Person? _selectedPerson;


    public PeopleModel(IPeopleService peopleService, IBooksService booksService)
    {
        _peopleService = peopleService;
        _booksService = booksService;

        SelectedPerson.ForEachAsync(action: SelectionChanged);
    }

    public async ValueTask SelectionChanged(Person? selectedPerson, CancellationToken ct)
    {
        _ct = ct;
        _selectedPerson = selectedPerson!;
        if (selectedPerson == null)
            return;

        IImmutableList<BookRead> BooksRead = await _booksService.GetBooksRead(selectedPerson, ct);
    }
    public IListFeed<Person> People => ListFeed
                                        .Async<Person>(_peopleService.GetPeopleAsync)
                                        .Selection(SelectedPerson);

    public IState<Person> SelectedPerson => State<Person>.Empty(this);

    public IListFeed<BookRead> BooksRead => ListFeed.Async<BookRead>(async (_ct) => await _booksService.GetBooksRead(_selectedPerson!, (CancellationToken)_ct));
}
