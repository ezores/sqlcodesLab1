using System;
using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using ImageHelper = Webflix.Helpers.ImageHelper;

namespace Webflix.ViewModels;

public class MovieDetailsViewModel : ViewModelBase
{
    public string Titre { get; set; } = "Spider-Man";
    public int AnneSortie { get; set; } = 2000;
    public ObservableCollection<string> Pays { get; set; } = new() { "États-Unis", "Canada" };
    public string LangeOriginale { get; set; } = "Anglais";
    public int DureeMinute { get; set; } = 199;
    public ObservableCollection<string> Genres { get; set; } = new() { "Humour", "Comédie", "Action", "Romance" };
    public string Realisateur { get; set; } = "Anonymous";
    public ObservableCollection<string> Scenaristes { get; set; } = new() { "S1", "S2", "S3" };
    public ObservableCollection<string> Acteurs { get; set; } = new() { "A1 en tant que P1", "A2 en tant que P2", "A3 en tant que P3" };
    public string Resume { get; set; } = "Un excellent film de Spider-Man";
    public Bitmap? AfficheURL { get; } = ImageHelper.LoadFromResource(new Uri("avares://Webflix/Assets/spiderman.jpg"));
    public ObservableCollection<string> BandeAnnonces { get; set; } = new() { "youtube.com", "youtube.com/spiderman" };

    public string TitreText { get; set; } = "Titre:";
    public string AnneSortieText { get; set; } = "Année de sortie:";
    public string PaysText { get; set; } = "Pays de production:";
    public string LangeOriginaleText { get; set; } = "Langue originale:";
    public string DureeMinuteText { get; set; } = "Durée (minutes):";
    public string GenresText { get; set; } = "Genres:";
    public string RealisateurText { get; set; } = "Réalisateur:";
    public string ScenaristesText { get; set; } = "Scénaristes:";
    public string ActeursText { get; set; } = "Acteurs et personnages:";
    public string ResumeText { get; set; } = "Résumé:";
}
