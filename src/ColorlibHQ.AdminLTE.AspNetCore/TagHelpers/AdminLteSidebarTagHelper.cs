using System.Text;
using System.Text.Encodings.Web;
using ColorlibHQ.AdminLTE.AspNetCore.Menu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace ColorlibHQ.AdminLTE.AspNetCore.TagHelpers;

/// <summary>
/// Server-renders the full AdminLTE sidebar (<c>&lt;aside class="app-sidebar"&gt;</c>)
/// from the config-driven menu, with active-item highlighting based on the current
/// request path. Usage: <c>&lt;adminlte-sidebar /&gt;</c>.
/// </summary>
[HtmlTargetElement("adminlte-sidebar", TagStructure = TagStructure.WithoutEndTag)]
public sealed class AdminLteSidebarTagHelper(
    IOptions<AdminLteOptions> options,
    IMenuService menuService,
    IHttpContextAccessor httpContextAccessor) : TagHelper
{
    private readonly AdminLteOptions _options = options.Value;

    /// <summary>Override the sidebar theme (<c>dark</c>/<c>light</c>).</summary>
    public string? Theme { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var http = httpContextAccessor.HttpContext;
        var user = http?.User;
        var menu = await menuService.GetMenuAsync(user);
        var currentPath = NormalizePath(http?.Request.Path.Value ?? "/");
        var theme = Theme ?? _options.SidebarTheme;

        var sb = new StringBuilder();
        var enc = HtmlEncoder.Default;

        sb.Append($"""<aside class="app-sidebar bg-body-secondary shadow" data-bs-theme="{enc.Encode(theme)}">""");

        // Brand
        sb.Append("""<div class="sidebar-brand"><a href="""")
          .Append(enc.Encode(_options.BrandUrl)).Append("""" class="brand-link">""");
        if (!string.IsNullOrEmpty(_options.LogoImage))
            sb.Append($"""<img src="{enc.Encode(_options.LogoImage)}" alt="{enc.Encode(_options.BrandText)}" class="brand-image opacity-75 shadow">""");
        else
            sb.Append("""<span class="brand-image opacity-75 shadow"></span>""");
        sb.Append($"""<span class="brand-text fw-light">{enc.Encode(_options.BrandText)}</span></a></div>""");

        // Menu
        sb.Append("""<div class="sidebar-wrapper"><nav class="mt-2" aria-label="Main navigation">""");
        sb.Append("""<ul class="nav sidebar-menu flex-column" data-lte-toggle="treeview" role="menu" id="navigation">""");
        foreach (var item in menu)
            RenderItem(sb, enc, item, currentPath);
        sb.Append("</ul></nav></div></aside>");

        output.TagName = null;
        output.Content.SetHtmlContent(sb.ToString());
    }

    private static void RenderItem(StringBuilder sb, HtmlEncoder enc, MenuItem item, string currentPath)
    {
        if (item.IsHeader)
        {
            sb.Append($"""<li class="nav-header">{enc.Encode(item.Header!)}</li>""");
            return;
        }

        if (item.HasChildren)
        {
            var open = ContainsActive(item, currentPath);
            sb.Append($"""<li class="nav-item{(open ? " menu-open" : "")}">""");
            sb.Append($"""<a href="#" class="nav-link{(open ? " active" : "")}">""");
            AppendIcon(sb, enc, item);
            sb.Append("<p>").Append(enc.Encode(item.Text ?? string.Empty));
            sb.Append("""<i class="nav-arrow bi bi-chevron-right"></i>""");
            AppendBadge(sb, enc, item);
            sb.Append("</p></a>");
            sb.Append("""<ul class="nav nav-treeview">""");
            foreach (var child in item.Children)
                RenderItem(sb, enc, child, currentPath);
            sb.Append("</ul></li>");
            return;
        }

        var active = IsActive(item, currentPath);
        var target = string.IsNullOrEmpty(item.Target) ? "" : $""" target="{enc.Encode(item.Target)}""";
        sb.Append("""<li class="nav-item">""");
        sb.Append($"""<a href="{enc.Encode(item.ResolvedHref)}"{target} class="nav-link{(active ? " active" : "")}">""");
        AppendIcon(sb, enc, item);
        sb.Append("<p>").Append(enc.Encode(item.Text ?? string.Empty));
        AppendBadge(sb, enc, item);
        sb.Append("</p></a></li>");
    }

    private static void AppendIcon(StringBuilder sb, HtmlEncoder enc, MenuItem item)
    {
        if (string.IsNullOrEmpty(item.Icon))
            return;
        var color = string.IsNullOrEmpty(item.IconColor) ? "" : $" text-{enc.Encode(item.IconColor)}";
        sb.Append($"""<i class="nav-icon {enc.Encode(item.Icon)}{color}"></i>""");
    }

    private static void AppendBadge(StringBuilder sb, HtmlEncoder enc, MenuItem item)
    {
        if (string.IsNullOrEmpty(item.Badge))
            return;
        sb.Append($"""<span class="nav-badge badge text-bg-{enc.Encode(item.BadgeColor)}">{enc.Encode(item.Badge)}</span>""");
    }

    private static bool IsActive(MenuItem item, string currentPath)
    {
        var href = item.ResolvedHref;
        return href is not ("#" or "") &&
               string.Equals(NormalizePath(href), currentPath, StringComparison.OrdinalIgnoreCase);
    }

    private static bool ContainsActive(MenuItem item, string currentPath) =>
        item.HasChildren ? item.Children.Any(c => ContainsActive(c, currentPath)) : IsActive(item, currentPath);

    private static string NormalizePath(string url)
    {
        var path = url.Split('?')[0].Split('#')[0].TrimEnd('/');
        return path.Length == 0 ? "/" : path;
    }
}
