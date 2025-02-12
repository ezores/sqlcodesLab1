using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Webflix.Views;

public partial class SearchView : UserControl
{
    public SearchView()
    {
        InitializeComponent();
    }

    private void OnClear(object? sender, RoutedEventArgs e)
    {
        TitleBox.Clear();
        ActorBox.Clear();
        DirectorBox.Clear();
        GenreComboBox.Clear();
        CountryComboBox.Clear();
        LanguageComboBox.Clear();
        FromDatePricker.Clear();
        ToDatePicker.Clear();
    }
}