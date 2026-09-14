namespace DevTools.Core.Abstractions;

/// <summary>
/// Metadata describing a tool shown in the shell navigation.
/// </summary>
public sealed record ToolInfo(string Id, string Name, string Description, ToolCategory Category);
