using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ColorlibHQ.AdminLTE.AspNetCore.TagHelpers;

/// <summary>
/// Server-rendered AdminLTE callout — a bordered note block.
/// Usage: <c>&lt;adminlte-callout theme="info" title="Tip"&gt;…&lt;/adminlte-callout&gt;</c>.
/// </summary>
[HtmlTargetElement("adminlte-callout")]
public sealed class CalloutTagHelper : TagHelper
{
    /// <summary>Bootstrap theme color (drives the left border).</summary>
    public string Theme { get; set; } = "info";

    /// <summary>Optional heading.</summary>
    public string? Title { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var enc = HtmlEncoder.Default;
        var body = (await output.GetChildContentAsync()).GetContent();
        var sb = new StringBuilder();

        sb.Append($"""<div class="callout callout-{enc.Encode(Theme)}">""");
        if (!string.IsNullOrEmpty(Title))
            sb.Append($"<h5>{enc.Encode(Title)}</h5>");
        sb.Append(body).Append("</div>");

        output.TagName = null;
        output.Content.SetHtmlContent(sb.ToString());
    }
}
