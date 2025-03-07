using ReactiveUI;

namespace Webflix.ViewModels;

public class PersonViewModel : ViewModelBase
{
    private string _biography = "Peter Parker est un jeune étudiant en sciences vivant à New York. Sa vie bascule lorsqu'il est mordu par une araignée radioactive, lui conférant des pouvoirs extraordinaires : une force surhumaine, une agilité accrue et la capacité de s’accrocher aux murs. Doté également d'un \"sens d’araignée\" lui permettant de détecter les dangers imminents, il conçoit des lance-toiles pour se déplacer à travers la ville.";

    public string Biography
    {
        get => _biography;
        set => this.RaiseAndSetIfChanged(ref _biography, value);
    }
}