using Avalonia.Platform.Storage;
using DevTools.Core.Abstractions;

namespace DevTools.Desktop.Services;

public sealed class FileDialogService : IFileDialogService
{
    public async Task<string?> OpenTextFileAsync(string title)
    {
        var storage = TopLevelProvider.GetTopLevel().StorageProvider;
        var files = await storage.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = title,
            AllowMultiple = false,
        });

        if (files.Count == 0)
            return null;

        await using var stream = await files[0].OpenReadAsync();
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }

    public async Task<bool> SaveTextFileAsync(string title, string suggestedName, string content)
    {
        var storage = TopLevelProvider.GetTopLevel().StorageProvider;
        var file = await storage.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = title,
            SuggestedFileName = suggestedName,
        });

        if (file is null)
            return false;

        await using var stream = await file.OpenWriteAsync();
        await using var writer = new StreamWriter(stream);
        await writer.WriteAsync(content);
        return true;
    }
}
