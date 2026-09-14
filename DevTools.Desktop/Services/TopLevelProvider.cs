using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace DevTools.Desktop.Services;

internal static class TopLevelProvider
{
    public static TopLevel GetTopLevel()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: { } window })
            return window;

        throw new InvalidOperationException("Main window is not available.");
    }
}
