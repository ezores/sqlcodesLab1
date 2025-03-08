using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using ReactiveUI;
using Webflix.Helpers;
using Webflix.Models.Entities;

namespace Webflix.ViewModels;

public class MovieTileViewModel(IHttpClientFactory httpClientFactory) : ViewModelBase
{
    public Film? Movie { get; set; } 
    
    private string _movieTitle = string.Empty;

    public string MovieTitle
    {
        get => _movieTitle;
        set => this.RaiseAndSetIfChanged(ref _movieTitle, value);
    }

    private Bitmap? _moviePoster;

    public Bitmap? MoviePoster
    {
        get => _moviePoster;
        set => this.RaiseAndSetIfChanged(ref _moviePoster, value);
    }

    public async Task LoadMoviePosterAsync()
    {
        if (string.IsNullOrEmpty(Movie?.AfficheUrl))
        {
            return;
        }

        try
        {
            await using var imageStream = await LoadCoverBitmapAsync();
            MoviePoster = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
        }
        catch (Exception _)
        {
            // ignored
        }
    }
    
    private async Task<Stream> LoadCoverBitmapAsync()
    {
        var client = httpClientFactory.CreateClient();
        var data = await client.GetByteArrayAsync(UrlHelper.EnsureHttps(Movie!.AfficheUrl));
        return new MemoryStream(data);
    }
}