using System.Text;

namespace DevTools.Core.Tools.Base64;

public static class Base64Converter
{
    public static string Encode(string input, bool urlSafe = false)
    {
        var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        return urlSafe ? encoded.TrimEnd('=').Replace('+', '-').Replace('/', '_') : encoded;
    }

    public static bool TryDecode(string input, out string result)
    {
        var normalized = input.Trim().Replace('-', '+').Replace('_', '/');
        normalized = normalized.PadRight(normalized.Length + (4 - normalized.Length % 4) % 4, '=');

        var buffer = new byte[normalized.Length];
        if (Convert.TryFromBase64String(normalized, buffer, out var written))
        {
            result = Encoding.UTF8.GetString(buffer, 0, written);
            return true;
        }

        result = string.Empty;
        return false;
    }
}
