using System;
using Avalonia.Media.Imaging;
using ImageHelper = Webflix.Helpers.ImageHelper;

namespace Webflix.ViewModels;


public class MovieDetailsViewModel : ViewModelBase
{
    public string Titre { get; set; } = "Spider-Man";
    public int AnneSortie { get; set; } = 2000;
    public string[] Pays { get; set; } = ["États-Unis", "Canada"];
    public string LangeOriginale { get; set; } = "Anglais";
    public int DureeMinute { get; set; } = 199;
    public string[] Genres { get; set; } = ["Humour", "Comédie", "Action", "Romance"];
    public string Realisateur { get; set; } = "Anonymous";
    public string[] Scenaristes { get; set; } = ["S1", "S2", "S3"];
    public string[] Acteurs { get; set; } = ["A1 en tant que p1", "A2 en tant que P2", "A3 en tant que P3"]; //TODO Convertir en liste acteur pour avoir les persos
    public string Resume { get; set; } = "Un excellent film de Spider-Man";
    public Bitmap? AfficheURL { get; } = ImageHelper.LoadFromResource(new Uri("avares://Webflix/Assets/spiderman.jpg"));
    public string[] BandeAnnonces { get; set; } = ["youtube.com", "youtube.com/spiderman"];
    
    
    public string TitreText { get; set; } = "Titre: ";
    public string AnneSortieText { get; set; } = "Année de sortie";
    public string PaysText { get; set; } = "Pays de production";
    public string LangeOriginaleText { get; set; } = "Langue original";
    public string DureeMinuteText { get; set; } = "Durée(minutes)";
    public string GenresText { get; set; } = "Genres";
    public string RealisateurText { get; set; } = "Réalisateur";
    public string ScenaristesText { get; set; } = "Scénariste";
    public string ActeursText { get; set; } = "Acteurs et personnages"; //TODO Convertir en liste acteur pour avoir les persos
    public string ResumeText { get; set; } = "Résumé";
    
    
    
    
    
    
    
    
    
    

}