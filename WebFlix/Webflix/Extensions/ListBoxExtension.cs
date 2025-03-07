using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;

namespace Webflix.Extensions;

public static class ListBoxExtension
{
    public static bool IsListBoxItemClicked(this PointerReleasedEventArgs e) => e.Source is not null &&
        (e.Source is ListBoxItem || ((Visual)e.Source).FindAncestorOfType<ListBoxItem>() is not null);
}