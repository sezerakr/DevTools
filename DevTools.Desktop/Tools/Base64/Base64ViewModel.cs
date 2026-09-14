using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevTools.Core.Abstractions;
using DevTools.Core.Tools.Base64;
using DevTools.Desktop.Infrastructure;

namespace DevTools.Desktop.Tools.Base64;

public partial class Base64ViewModel(IClipboardService clipboard) : ToolViewModelBase
{
    public override ToolInfo Info { get; } = new( "base64", "Base64 Encode / Decode", "Encode text to Base64 or decode Base64 to text", ToolCategory.Encoders);

    [ObservableProperty]
    public partial string Input { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Output { get; set; } = string.Empty;

    [ObservableProperty]
    public partial bool IsDecodeMode { get; set; }

    [ObservableProperty]
    public partial bool UrlSafe { get; set; }

    [ObservableProperty]
    public partial string? Error { get; set; }

    partial void OnInputChanged(string value) => Convert();
    partial void OnIsDecodeModeChanged(bool value) => Convert();
    partial void OnUrlSafeChanged(bool value) => Convert();

    private void Convert()
    {
        Error = null;

        if (string.IsNullOrEmpty(Input))
        {
            Output = string.Empty;
            return;
        }

        if (!IsDecodeMode)
        {
            Output = Base64Converter.Encode(Input, UrlSafe);
        }
        else if (Base64Converter.TryDecode(Input, out var decoded))
        {
            Output = decoded;
        }
        else
        {
            Output = string.Empty;
            Error = "Invalid Base64 input";
        }
    }

    [RelayCommand]
    private async Task PasteAsync() => Input = await clipboard.GetTextAsync() ?? string.Empty;

    [RelayCommand]
    private Task CopyAsync() => clipboard.SetTextAsync(Output);

    [RelayCommand]
    private void Swap()
    {
        var output = Output;
        IsDecodeMode = !IsDecodeMode;
        Input = output;
    }
}
