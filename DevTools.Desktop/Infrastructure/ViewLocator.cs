using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace DevTools.Desktop.Infrastructure;

/// <summary>
/// Maps view models to views using an explicit registry (no reflection, trim/AOT safe).
/// </summary>
public sealed class ViewLocator : IDataTemplate
{
    private readonly Dictionary<Type, Func<Control>> _factories = new();

    public void Register<TViewModel, TView>()
        where TViewModel : ViewModelBase
        where TView : Control, new()
        => _factories[typeof(TViewModel)] = static () => new TView();

    public Control? Build(object? param)
    {
        if (param is null)
            return null;

        return _factories.TryGetValue(param.GetType(), out var factory)
            ? factory()
            : new TextBlock { Text = "View not registered: " + param.GetType().Name };
    }

    public bool Match(object? data) => data is ViewModelBase;
}
