using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Webflix.ViewModels;

namespace Webflix.Views;

public partial class MovieGridView : UserControl
{
    private new MovieGridViewModel? DataContext => base.DataContext as MovieGridViewModel;
    
    public MovieGridView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        
        MovieGrid.AddHandler(PointerReleasedEvent, OnMovieGridPointerReleased, RoutingStrategies.Tunnel);
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        
        MovieGrid.RemoveHandler(PointerReleasedEvent, OnMovieGridPointerReleased);
    }

    private void OnMovieGridPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (e.Source is not null &&
            (e.Source is ListBoxItem || ((Visual)e.Source).FindAncestorOfType<ListBoxItem>() is not null))
        {
            DataContext?.TriggerItem.Execute().Subscribe();
        }
    }
}