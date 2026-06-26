using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ColorlibHQ.AdminLTE.AspNetCore.TagHelpers;

/// <summary>
/// Server-rendered Bootstrap/AdminLTE alert.
/// Usage: <c>&lt;adminlte-alert theme="success" title="Done" icon="bi-check-circle-fill" dismissible="true"&gt;…&lt;/adminlte-alert&gt;</c>.
/// </summary>
[HtmlTargetElement("adminlte-alert")]
public sealed class AlertTagHelper : TagHelper
{
    /// <summary>Bootstrap theme color.</summary>
    public string Theme { get; set; } = "info";

    /// <summary>Optional heading.</summary>
    public string? Title { get; set; }

    /// <summary>Optional Bootstrap Icons class for the heading.</summary>
    public string? Icon { get; set; }

    /// <summary>Show a Bootstrap dismiss button.</summary>
    public bool Dismissible { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var enc = HtmlEncoder.Default;
        var body = (await output.GetChildContentAsync()).GetContent();
        var sb = new StringBuilder();

        sb.Append($"""<div class="alert alert-{enc.Encode(Theme)}{(Dismissible ? " alert-dismissible" : "")}" role="alert">""");
        if (!string.IsNullOrEmpty(Title) || !string.IsNullOrEmpty(Icon))
        {
            sb.Append("<h5>");
            if (!string.IsNullOrEmpty(Icon))
                sb.Append($"""<i class="bi {enc.Encode(Icon)} me-2"></i>""");
            sb.Append(enc.Encode(Title ?? string.Empty)).Append("</h5>");
        }
        sb.Append(body);
        if (Dismissible)
            sb.Append("""<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>""");
        sb.Append("</div>");

        output.TagName = null;
        output.Content.SetHtmlContent(sb.ToString());
    }
}
