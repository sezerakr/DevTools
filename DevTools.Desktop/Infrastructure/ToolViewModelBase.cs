using DevTools.Core.Abstractions;

namespace DevTools.Desktop.Infrastructure;

/// <summary>
/// Base class for every tool page. The shell lists all registered tools by their <see cref="Info"/>.
/// </summary>
public abstract class ToolViewModelBase : ViewModelBase
{
    public abstract ToolInfo Info { get; }
}
