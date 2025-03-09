using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Prism.Regions;
using ReactiveUI;
using Webflix.Helpers;
using Webflix.Models.Entities;

namespace Webflix.ViewModels;

public class PersonViewModel(IHttpClientFactory clientFactory) : ViewModelBase
{
    private Personne? _person;
    
    private string _biography = string.Empty;

    public string Biography
    {
        get => _biography;
        set => this.RaiseAndSetIfChanged(ref _biography, value);
    }

    private string _name = string.Empty;
    private string _birthday = string.Empty;
    private string _birthPlace = string.Empty;
    
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string Birthday
    {
        get => _birthday;
        set => this.RaiseAndSetIfChanged(ref _birthday, value);
    }

    public string BirthPlace
    {
        get => _birthPlace;
        set => this.RaiseAndSetIfChanged(ref _birthPlace, value);
    }
    
    private Bitmap? _personPhoto;

    public Bitmap? PersonPhoto
    {
        get => _personPhoto;
        set => this.RaiseAndSetIfChanged(ref _personPhoto, value);
    }

    public override async void OnNavigatedTo(NavigationContext navigationContext)
    {
        base.OnNavigatedTo(navigationContext);

        if (navigationContext.Parameters[MovieViewModel.PERSON_PARAMETER] is Personne person)
        {
            _person = person;
        }

        await InitView();
    }

    private async Task InitView()
    {
        if (_person is null)
        {
            return;
        }

        Name = _person.Nom;
        Biography = _person.Biographie;
        BirthPlace = _person.LieuNaissance;
        Birthday = _person.DateNaissance is null ? "Unknown" : $"{_person.DateNaissance.ToString()}";

        await LoadPhotoAsync();
    }

    private async Task LoadPhotoAsync()
    {
        if (string.IsNullOrEmpty(_person?.Photo))
        {
            return;
        }
    
        try
        {
            await using var imageStream = await LoadBitmapAsync();
            PersonPhoto = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
        }
        catch (Exception _)
        {
            // ignored
        }
    }
    
    private async Task<Stream> LoadBitmapAsync()
    {
        var client = clientFactory.CreateClient();
        var data = await client.GetByteArrayAsync(UrlHelper.EnsureHttps(_person!.Photo));
        return new MemoryStream(data);
    }
}