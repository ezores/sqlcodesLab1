using System;
using System.Reactive;
using Avalonia.Media.Imaging;
using Prism.Regions;
using ReactiveUI;
using Webflix.Helpers;
using Webflix.Resources;
using Webflix.Views;

namespace Webflix.ViewModels;

public class PersonDetailsViewModel : ViewModelBase
{
    public string Nom { get; set; } = "Anonymous";
    public DateTime DateNaissance { get; set; } = new DateTime(1999, 5, 1);
    public string LieuNaissance { get; set; } = "Quebec";
    public Bitmap? Photo { get; } = ImageHelper.LoadFromResource(new Uri("avares://Webflix/Assets/spiderman.jpg"));
    public string Biographie { get; set; } = "Je suis un réalisateur célèbre et mysterieux";
    
    public string NomText { get; set; } = "Nom:";
    public string DateNaissanceText { get; set; } = "Date de naissance:";
    public string LieuNaissanceText { get; set; } = "Lieu de naissance:";
    public string BiographieText { get; set; } = "Biographie:";

    
    
    
    
    private readonly IRegionManager _regionManager;
    
    public ReactiveCommand<Unit, Unit> OpenMovieDetailsCommand { get; set; }
    
    public PersonDetailsViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        
        OpenMovieDetailsCommand = ReactiveCommand.Create(OpenMovieDetailsCommandExecute);
    }

    private void OpenMovieDetailsCommandExecute()
    {
        _regionManager.RequestNavigate(Regions.MainRegion, nameof(MovieDetailsView));
    }
}