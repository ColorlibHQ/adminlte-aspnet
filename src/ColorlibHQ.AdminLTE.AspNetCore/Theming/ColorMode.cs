namespace ColorlibHQ.AdminLTE.AspNetCore.Theming;

/// <summary>The three AdminLTE color-mode choices, persisted to localStorage as <c>lte-theme</c>.</summary>
public enum ColorMode
{
    /// <summary>Follow the OS <c>prefers-color-scheme</c> setting.</summary>
    Auto,

    /// <summary>Force the light theme (<c>data-bs-theme="light"</c>).</summary>
    Light,

    /// <summary>Force the dark theme (<c>data-bs-theme="dark"</c>).</summary>
    Dark,
}

internal static class ColorModeExtensions
{
    /// <summary>The lowercase token used in markup and localStorage (<c>light</c>/<c>dark</c>/<c>auto</c>).</summary>
    public static string ToToken(this ColorMode mode) => mode switch
    {
        ColorMode.Light => "light",
        ColorMode.Dark => "dark",
        _ => "auto",
    };

    public static ColorMode Parse(string? token) => token?.Trim().ToLowerInvariant() switch
    {
        "light" => ColorMode.Light,
        "dark" => ColorMode.Dark,
        _ => ColorMode.Auto,
    };

    /// <summary>The Bootstrap Icons class representing the mode in the toggle button.</summary>
    public static string ToIcon(this ColorMode mode) => mode switch
    {
        ColorMode.Light => "bi-sun-fill",
        ColorMode.Dark => "bi-moon-fill",
        _ => "bi-circle-half",
    };
}
