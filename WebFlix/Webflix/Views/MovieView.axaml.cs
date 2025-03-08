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
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        
        TrailerList.RemoveHandler(PointerReleasedEvent, OnTrailerPointerReleased);
        ActorList.RemoveHandler(PointerReleasedEvent, OnActorPointerReleased);
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
    
}