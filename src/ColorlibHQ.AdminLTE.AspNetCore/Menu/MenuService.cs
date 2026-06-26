using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace ColorlibHQ.AdminLTE.AspNetCore.Menu;

/// <summary>
/// Default <see cref="IMenuService"/>: reads the menu from <see cref="AdminLteOptions"/>
/// and filters items by their <see cref="MenuItem.Policy"/> / <see cref="MenuItem.Role"/>.
/// </summary>
public sealed class MenuService(
    IOptionsMonitor<AdminLteOptions> options,
    IAuthorizationService authorization) : IMenuService
{
    public async ValueTask<IReadOnlyList<MenuItem>> GetMenuAsync(ClaimsPrincipal? user = null)
    {
        var menu = options.CurrentValue.Menu;
        if (user is null)
            return menu.ToList();

        var result = new List<MenuItem>(menu.Count);
        foreach (var item in menu)
        {
            var filtered = await FilterAsync(item, user);
            if (filtered is not null)
                result.Add(filtered);
        }

        return result;
    }

    public async ValueTask<IReadOnlyList<CommandItem>> GetCommandsAsync(ClaimsPrincipal? user = null)
    {
        var menu = await GetMenuAsync(user);
        var commands = new List<CommandItem>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        Flatten(menu, group: null, commands, seen);
        return commands;
    }

    private static void Flatten(
        IEnumerable<MenuItem> items, string? group, List<CommandItem> output, HashSet<string> seen)
    {
        foreach (var item in items)
        {
            if (item.IsHeader)
                continue;

            if (item.HasChildren)
            {
                Flatten(item.Children, item.Text, output, seen);
                continue;
            }

            var href = item.ResolvedHref;
            if (href is "#" || string.IsNullOrWhiteSpace(item.Text) || !seen.Add(href))
                continue;

            output.Add(new CommandItem(item.Text!, href, item.Icon, group));
        }
    }

    private async ValueTask<MenuItem?> FilterAsync(MenuItem item, ClaimsPrincipal user)
    {
        if (!await IsVisibleAsync(item, user))
            return null;

        if (!item.HasChildren)
            return item;

        // Recurse into children; drop empty groups whose children were all filtered out.
        var keptChildren = new List<MenuItem>(item.Children.Count);
        foreach (var child in item.Children)
        {
            var filtered = await FilterAsync(child, user);
            if (filtered is not null)
                keptChildren.Add(filtered);
        }

        if (keptChildren.Count == 0)
            return null;

        return new MenuItem
        {
            Header = item.Header,
            Text = item.Text,
            Url = item.Url,
            Route = item.Route,
            Icon = item.Icon,
            IconColor = item.IconColor,
            Badge = item.Badge,
            BadgeColor = item.BadgeColor,
            Target = item.Target,
            Policy = item.Policy,
            Role = item.Role,
            Children = keptChildren,
        };
    }

    private async ValueTask<bool> IsVisibleAsync(MenuItem item, ClaimsPrincipal user)
    {
        if (!string.IsNullOrWhiteSpace(item.Role) && !user.IsInRole(item.Role))
            return false;

        if (!string.IsNullOrWhiteSpace(item.Policy))
        {
            var authorized = await authorization.AuthorizeAsync(user, item.Policy);
            if (!authorized.Succeeded)
                return false;
        }

        return true;
    }
}
