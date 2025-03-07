using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Webflix.Extensions;
using Webflix.ViewModels;

namespace Webflix.Views;

public partial class MovieView : UserControl
{
    private new MovieViewModel? DataContext => base.DataContext as MovieViewModel;
    
    public MovieView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        
        TrailerList.AddHandler(PointerReleasedEvent, OnTrailerPointerReleased, RoutingStrategies.Tunnel);
        ActorList.AddHandler(PointerReleasedEvent, OnActorPointerReleased, RoutingStrategies.Tunnel);
        ScreenWriterList.AddHandler(PointerReleasedEvent, OnScreenwriterPointerReleased, RoutingStrategies.Tunnel);
    }

    private void OnTrailerPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (e.IsListBoxItemClicked())
        {
            DataContext?.TrailerCommand.Execute().Subscribe();
        }
    }
    
    private void OnActorPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (e.IsListBoxItemClicked())
        {
            DataContext?.ActorCommand.Execute().Subscribe();
        }
    }
    
    private void OnScreenwriterPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (e.IsListBoxItemClicked())
        {
            DataContext?.ScreenwriterCommand.Execute().Subscribe();
        }
    }
}