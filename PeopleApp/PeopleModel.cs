namespace PeopleApp;
public partial record PeopleModel
{
    private IPeopleService _peopleService;
    private IBooksService _booksService;


    public PeopleModel(IPeopleService peopleService, IBooksService booksService)
    {
        _peopleService = peopleService;
        _booksService = booksService;


        SelectedPerson.ForEachAsync(action: SelectionChanged);
    }

    public async ValueTask SelectionChanged(Person? selectedPerson, CancellationToken ct)
    {
        //_selectedPerson = selectedPerson!;
        //if (selectedPerson == null)
        //   return;

        IImmutableList<BookRead> BooksRead = await _booksService.GetBooksRead(selectedPerson!);
    }
    public IListFeed<Person> People => ListFeed
                                        .Async<Person>(_peopleService.GetPeople)
                                        .Selection(SelectedPerson);

    public IState<Person> SelectedPerson => State<Person>.Empty(this);
   


    public IListFeed<BookRead> BooksRead => ListFeed.Async<BookRead>(_booksService.GetBooksRead(SelectedPerson));
    //public IFeed<IImmutableList<BookRead>> BooksRead => SelectedPerson.SelectAsync(<BookRead>selectedPerson => (BookRead) _booksService.GetBooksRead(selectedPerson));
}
