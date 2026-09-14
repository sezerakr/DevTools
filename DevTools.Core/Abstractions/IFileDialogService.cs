namespace DevTools.Core.Abstractions;

public interface IFileDialogService
{
    /// <summary>Opens a file picker and returns the selected file content, or null if cancelled.</summary>
    Task<string?> OpenTextFileAsync(string title);

    /// <summary>Opens a save picker and writes the content. Returns false if cancelled.</summary>
    Task<bool> SaveTextFileAsync(string title, string suggestedName, string content);
}
