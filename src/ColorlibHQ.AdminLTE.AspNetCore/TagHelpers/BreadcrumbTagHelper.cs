using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ColorlibHQ.AdminLTE.AspNetCore.TagHelpers;

/// <summary>
/// Server-rendered breadcrumb trail. Pass the crumbs from view code:
/// <c>&lt;adminlte-breadcrumb items="Model.Crumbs" /&gt;</c>. The last crumb renders as the active page.
/// </summary>
[HtmlTargetElement("adminlte-breadcrumb", TagStructure = TagStructure.WithoutEndTag)]
public sealed class BreadcrumbTagHelper : TagHelper
{
    /// <summary>One breadcrumb in the trail.</summary>
    public sealed record Crumb(string Label, string? Href = null);

    /// <summary>The crumbs, in order.</summary>
    public IReadOnlyList<Crumb> Items { get; set; } = Array.Empty<Crumb>();

    /// <summary>Extra classes on the <c>&lt;ol&gt;</c>.</summary>
    public string? Class { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var enc = HtmlEncoder.Default;
        var sb = new StringBuilder();
        var extra = string.IsNullOrEmpty(Class) ? "" : $" {enc.Encode(Class)}";

        sb.Append($"""<nav aria-label="breadcrumb"><ol class="breadcrumb{extra}">""");
        for (var i = 0; i < Items.Count; i++)
        {
            var crumb = Items[i];
            var isLast = i == Items.Count - 1;
            sb.Append($"""<li class="breadcrumb-item{(isLast ? " active" : "")}"{(isLast ? """ aria-current="page" """.TrimEnd() : "")}>""");
            if (!isLast && !string.IsNullOrEmpty(crumb.Href))
                sb.Append($"""<a href="{enc.Encode(crumb.Href)}">{enc.Encode(crumb.Label)}</a>""");
            else
                sb.Append(enc.Encode(crumb.Label));
            sb.Append("</li>");
        }
        sb.Append("</ol></nav>");

        output.TagName = null;
        output.Content.SetHtmlContent(sb.ToString());
    }
}
