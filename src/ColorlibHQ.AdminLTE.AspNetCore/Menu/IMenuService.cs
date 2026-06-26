using System.Security.Claims;

namespace ColorlibHQ.AdminLTE.AspNetCore.Menu;

/// <summary>
/// Provides the sidebar menu, filtered for the current user's authorization,
/// and flattens it for the command palette.
/// </summary>
public interface IMenuService
{
    /// <summary>
    /// Returns the configured menu with any items the <paramref name="user"/> is
    /// not authorized to see removed. Pass <c>null</c> to skip authorization filtering.
    /// </summary>
    ValueTask<IReadOnlyList<MenuItem>> GetMenuAsync(ClaimsPrincipal? user = null);

    /// <summary>
    /// Flattens the (authorized) menu tree into a list of searchable command entries
    /// for the ⌘K palette. Headers and <c>#</c> links are skipped.
    /// </summary>
    ValueTask<IReadOnlyList<CommandItem>> GetCommandsAsync(ClaimsPrincipal? user = null);
}

/// <summary>A flattened, searchable navigation target for the command palette.</summary>
/// <param name="Label">Display text.</param>
/// <param name="Href">Navigation target.</param>
/// <param name="Icon">Optional Bootstrap Icons class.</param>
/// <param name="Group">Optional parent group label (shown as a badge).</param>
public readonly record struct CommandItem(string Label, string Href, string? Icon, string? Group);
