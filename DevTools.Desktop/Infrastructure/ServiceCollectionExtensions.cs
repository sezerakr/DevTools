using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace DevTools.Desktop.Infrastructure;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers a tool view model (singleton, keeps state when switching tools) and its view.
    /// </summary>
    public static IServiceCollection AddTool<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel,
        TView>(this IServiceCollection services, ViewLocator locator)
        where TViewModel : ToolViewModelBase
        where TView : Control, new()
    {
        locator.Register<TViewModel, TView>();
        services.AddSingleton<TViewModel>();
        services.AddSingleton<ToolViewModelBase>(sp => sp.GetRequiredService<TViewModel>());
        return services;
    }
}
