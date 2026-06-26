using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ColorlibHQ.AdminLTE.AspNetCore.TagHelpers;

/// <summary>
/// Server-rendered AdminLTE small-box stat widget.
/// Usage: <c>&lt;adminlte-small-box title="150" text="New Orders" icon="bi-bag" theme="primary" url="/orders" /&gt;</c>.
/// </summary>
[HtmlTargetElement("adminlte-small-box", TagStructure = TagStructure.WithoutEndTag)]
public sealed class SmallBoxTagHelper : TagHelper
{
    /// <summary>The big stat / number.</summary>
    public string? Title { get; set; }

    /// <summary>Label under the stat.</summary>
    public string? Text { get; set; }

    /// <summary>Bootstrap Icons class.</summary>
    public string? Icon { get; set; }

    /// <summary>Bootstrap theme color.</summary>
    public string Theme { get; set; } = "primary";

    /// <summary>"More info" link target.</summary>
    public string? Url { get; set; }

    /// <summary>"More info" link text.</summary>
    public string UrlText { get; set; } = "More info";

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var enc = HtmlEncoder.Default;
        var sb = new System.Text.StringBuilder();

        sb.Append($"""<div class="small-box text-bg-{enc.Encode(Theme)}"><div class="inner"><h3>""")
          .Append(enc.Encode(Title ?? string.Empty)).Append("</h3>");
        if (!string.IsNullOrEmpty(Text))
            sb.Append("<p>").Append(enc.Encode(Text)).Append("</p>");
        sb.Append("</div>");
        if (!string.IsNullOrEmpty(Icon))
            sb.Append($"""<i class="small-box-icon bi {enc.Encode(Icon)}"></i>""");
        if (!string.IsNullOrEmpty(Url))
        {
            var linkColor = Theme is "warning" or "light" or "info" ? "link-dark" : "link-light";
            sb.Append($"""<a href="{enc.Encode(Url)}" class="small-box-footer {linkColor}">""")
              .Append(enc.Encode(UrlText))
              .Append(""" <i class="bi bi-link-45deg"></i></a>""");
        }
        sb.Append("</div>");

        output.TagName = null;
        output.Content.SetHtmlContent(sb.ToString());
    }
}
