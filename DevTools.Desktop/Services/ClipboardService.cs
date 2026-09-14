using Avalonia.Input.Platform;
using DevTools.Core.Abstractions;

namespace DevTools.Desktop.Services;

public sealed class ClipboardService : IClipboardService
{
    public async Task<string?> GetTextAsync()
    {
        var clipboard = TopLevelProvider.GetTopLevel().Clipboard;
        return clipboard is null ? null : await clipboard.TryGetTextAsync();
    }

    public async Task SetTextAsync(string text)
    {
        var clipboard = TopLevelProvider.GetTopLevel().Clipboard;
        if (clipboard is not null)
            await clipboard.SetTextAsync(text);
    }
}
