using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using DevTools.Desktop.Infrastructure;

namespace DevTools.Desktop.Shell;

public partial class MainViewModel : ViewModelBase
{
    private readonly IReadOnlyList<ToolViewModelBase> _allTools;

    public MainViewModel(IEnumerable<ToolViewModelBase> tools)
    {
        _allTools = tools
            .OrderBy(t => t.Info.Category)
            .ThenBy(t => t.Info.Name)
            .ToList();

        Tools = new ObservableCollection<ToolViewModelBase>(_allTools);
        SelectedTool = Tools.FirstOrDefault();
    }

    public ObservableCollection<ToolViewModelBase> Tools { get; }

    [ObservableProperty]
    public partial ToolViewModelBase? SelectedTool { get; set; }

    [ObservableProperty]
    public partial string SearchText { get; set; } = string.Empty;

    partial void OnSearchTextChanged(string value)
    {
        var selected = SelectedTool;
        Tools.Clear();

        foreach (var tool in _allTools.Where(t => string.IsNullOrWhiteSpace(value)
                     || t.Info.Name.Contains(value, StringComparison.OrdinalIgnoreCase)
                     || t.Info.Description.Contains(value, StringComparison.OrdinalIgnoreCase)))
        {
            Tools.Add(tool);
        }

        SelectedTool = selected is not null && Tools.Contains(selected) ? selected : Tools.FirstOrDefault();
    }
}
