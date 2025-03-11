using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Webflix.ViewModels;

namespace Webflix.Views;

public partial class LoadingOverlay : UserControl
{
    private new MainWindowViewModel? DataContext => base.DataContext as MainWindowViewModel;
    
    public LoadingOverlay()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);

        if (DataContext is null)
        {
            return;
        }
        
        DataContext.ShowLoading = ShowLoading;
        DataContext.HideLoading = HideLoading;
    }

    private void ShowLoading()
    {
        LoadingIndicator.IsActive = true;
        Opacity = 0.55;
    }

    private void HideLoading()
    {
        LoadingIndicator.IsActive = false;
        Opacity = 0;
    }
}