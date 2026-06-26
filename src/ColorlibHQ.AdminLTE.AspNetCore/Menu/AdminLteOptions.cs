namespace ColorlibHQ.AdminLTE.AspNetCore.Menu;

/// <summary>
/// Strongly-typed AdminLTE configuration, bound from <c>appsettings.json</c>
/// via the Options pattern (section name <see cref="SectionName"/>).
/// </summary>
public sealed class AdminLteOptions
{
    /// <summary>The default configuration section name (<c>"AdminLte"</c>).</summary>
    public const string SectionName = "AdminLte";

    /// <summary>Text shown next to the sidebar logo. Defaults to <c>AdminLTE 4</c>.</summary>
    public string BrandText { get; set; } = "AdminLTE 4";

    /// <summary>URL the brand/logo links to.</summary>
    public string BrandUrl { get; set; } = "/";

    /// <summary>Optional logo image URL. When null, a placeholder square is rendered.</summary>
    public string? LogoImage { get; set; }

    /// <summary>Sidebar color theme: <c>dark</c> or <c>light</c>.</summary>
    public string SidebarTheme { get; set; } = "dark";

    /// <summary>Breakpoint at which the sidebar expands: <c>sm</c>, <c>md</c>, <c>lg</c>, <c>xl</c>, <c>xxl</c>.</summary>
    public string SidebarBreakpoint { get; set; } = "lg";

    /// <summary>Apply the <c>layout-fixed</c> class (sticky wrapper, scrollable content).</summary>
    public bool LayoutFixed { get; set; } = true;

    /// <summary>Apply the <c>fixed-header</c> class.</summary>
    public bool FixedHeader { get; set; } = true;

    /// <summary>Apply the <c>fixed-footer</c> class.</summary>
    public bool FixedFooter { get; set; }

    /// <summary>Show the light/dark/auto color-mode dropdown in the topbar.</summary>
    public bool ColorModeToggle { get; set; } = true;

    /// <summary>Initial color mode before any persisted preference is read: <c>light</c>, <c>dark</c> or <c>auto</c>.</summary>
    public string DefaultColorMode { get; set; } = "auto";

    /// <summary>Show the ⌘K command palette trigger in the topbar.</summary>
    public bool CommandPalette { get; set; } = true;

    /// <summary>Footer copyright/right-hand text.</summary>
    public string FooterText { get; set; } = "AdminLTE.io";

    /// <summary>The sidebar menu tree.</summary>
    public IList<MenuItem> Menu { get; set; } = new List<MenuItem>();
}
