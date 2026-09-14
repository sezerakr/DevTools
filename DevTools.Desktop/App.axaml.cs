using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DevTools.Core.Abstractions;
using DevTools.Desktop.Infrastructure;
using DevTools.Desktop.Services;
using DevTools.Desktop.Shell;
using DevTools.Desktop.Tools.Base64;
using Microsoft.Extensions.DependencyInjection;

namespace DevTools.Desktop;

public partial class App : Application
{
    public IServiceProvider Services { get; private set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var viewLocator = new ViewLocator();
        DataTemplates.Add(viewLocator);

        Services = ConfigureServices(viewLocator);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Services.GetRequiredService<MainViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static ServiceProvider ConfigureServices(ViewLocator viewLocator)
    {
        var services = new ServiceCollection();

        // Platform services
        services.AddSingleton<IClipboardService, ClipboardService>();
        services.AddSingleton<IFileDialogService, FileDialogService>();

        // Shell
        services.AddSingleton<MainViewModel>();

        // Tools: one line per tool
        services.AddTool<Base64ViewModel, Base64View>(viewLocator);

        return services.BuildServiceProvider();
    }
}
