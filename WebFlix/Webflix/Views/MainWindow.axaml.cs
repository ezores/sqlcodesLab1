using Avalonia.Controls;
using Webflix.ViewModels;

namespace Webflix.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        WindowState = WindowState.Maximized;
        ExtendClientAreaToDecorationsHint = true;
    }

    private new MainWindowViewModel? DataContext => base.DataContext as MainWindowViewModel;
}