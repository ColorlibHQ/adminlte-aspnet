namespace ColorlibHQ.AdminLTE.AspNetCore.Menu;

/// <summary>
/// A single entry in the AdminLTE sidebar menu. Bound from configuration
/// (the Options pattern) or built in code. Mirrors the Laravel port's
/// config-driven menu item schema (text / route / icon / badge / submenu / can).
/// </summary>
public sealed class MenuItem
{
    /// <summary>
    /// When set, this item renders as a non-clickable section header
    /// (<c>&lt;li class="nav-header"&gt;</c>) and most other fields are ignored.
    /// </summary>
    public string? Header { get; set; }

    /// <summary>The visible link label.</summary>
    public string? Text { get; set; }

    /// <summary>
    /// Destination URL. Use this for raw/absolute links. For Blazor routing,
    /// this is bound directly to an anchor / <c>NavLink</c> <c>href</c>.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Optional named route key. Ports that resolve named routes can use this;
    /// when both <see cref="Url"/> and <see cref="Route"/> are set, <see cref="Url"/> wins.
    /// </summary>
    public string? Route { get; set; }

    /// <summary>Bootstrap Icons class, e.g. <c>bi bi-speedometer</c>.</summary>
    public string? Icon { get; set; }

    /// <summary>Optional text color suffix applied to the icon, e.g. <c>primary</c>.</summary>
    public string? IconColor { get; set; }

    /// <summary>Badge text/number rendered at the end of the row.</summary>
    public string? Badge { get; set; }

    /// <summary>Bootstrap theme for the badge background, e.g. <c>danger</c>. Defaults to <c>secondary</c>.</summary>
    public string BadgeColor { get; set; } = "secondary";

    /// <summary>Anchor <c>target</c> attribute (e.g. <c>_blank</c>).</summary>
    public string? Target { get; set; }

    /// <summary>
    /// Authorization policy name required to see this item. When set, the menu
    /// service hides the item unless the current user satisfies the policy.
    /// </summary>
    public string? Policy { get; set; }

    /// <summary>
    /// When set, the item is shown only if the current user is in this role
    /// (convenience shortcut; combine with <see cref="Policy"/> as needed).
    /// </summary>
    public string? Role { get; set; }

    /// <summary>Nested child items. Presence of children renders a collapsible treeview.</summary>
    public IList<MenuItem> Children { get; set; } = new List<MenuItem>();

    /// <summary>True when this entry is a section header.</summary>
    public bool IsHeader => !string.IsNullOrWhiteSpace(Header);

    /// <summary>True when this entry has nested children (renders a treeview group).</summary>
    public bool HasChildren => Children is { Count: > 0 };

    /// <summary>Resolved link target: <see cref="Url"/> if present, otherwise <see cref="Route"/>, otherwise <c>#</c>.</summary>
    public string ResolvedHref => Url ?? Route ?? "#";
}
