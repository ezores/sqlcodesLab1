using ReactiveUI;

namespace Webflix.ViewModels;

public class MovieTileViewModel : ViewModelBase
{
    private string _movieTitle = string.Empty;

    public string MovieTitle
    {
        get => _movieTitle;
        set => this.RaiseAndSetIfChanged(ref _movieTitle, value);
    }

    //TODO: handle images (see doc)
}