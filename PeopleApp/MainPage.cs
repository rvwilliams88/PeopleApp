using Microsoft.UI.Xaml.Controls;
using Uno.Toolkit.UI;

namespace PeopleApp;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.DataContext = new BindablePeopleModel(new PeopleService(),new BooksService());
        this.DataContext<BindablePeopleModel>((page, vm) => page
            .Background(ThemeResource.Get<Brush>("ApplicationPageBackgroundThemeBrush"))
            .Content(
                new StackPanel()
                .VerticalAlignment(VerticalAlignment.Center)
                .HorizontalAlignment(HorizontalAlignment.Center)
                .Orientation(Orientation.Vertical)
                .Children(
                    new FeedView()
                    .Source(x => x.Binding(() => vm.People))
                    .ValueTemplate<ComboBox>((sample) =>
                        new ComboBox()
                        .Name("authorList")
                        .ItemsSource(x => x.Binding("Data"))
                        .Header(
                            new Button()
                            .Content("Refresh")
                            .Command<Button>(configureProperty: x => x.Binding("Refresh"))
                        )
                        .ItemTemplate<string>(person =>
                            new StackPanel()
                            .Orientation(Orientation.Horizontal)
                            .Children(
                                new TextBlock()
                                .Text(x => x.Binding("FullName"))
                            )
                        )
                    ),
                    new FeedView()
                    .Source(x => x.Binding(() => vm.BooksRead))
                    .ValueTemplate<ListView>((sample) =>
                        new ListView()
                        .ItemsSource(x => x.Binding("Data"))
                        .ItemTemplate<string>(book =>
                            new StackPanel()
                            .Orientation(Orientation.Vertical)
                            .Children(
                                new TextBlock()
                                .Text(x => x.Binding("Title"))

                            )
                        )
                    )
                )
            )
            
        );
    }
}
