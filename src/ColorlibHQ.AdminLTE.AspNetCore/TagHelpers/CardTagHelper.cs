using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ColorlibHQ.AdminLTE.AspNetCore.TagHelpers;

/// <summary>
/// Server-rendered AdminLTE card for MVC / Razor Pages.
/// Usage: <c>&lt;adminlte-card title="Sales" theme="primary"&gt;…body…&lt;/adminlte-card&gt;</c>.
/// </summary>
[HtmlTargetElement("adminlte-card")]
public sealed class CardTagHelper : TagHelper
{
    /// <summary>Header title (omitted = no header).</summary>
    public string? Title { get; set; }

    /// <summary>Bootstrap Icons class before the title.</summary>
    public string? Icon { get; set; }

    /// <summary>Bootstrap theme color.</summary>
    public string? Theme { get; set; }

    /// <summary><c>default</c>, <c>outline</c> or <c>solid</c>.</summary>
    public string Variant { get; set; } = "default";

    /// <summary>Add a collapse tool.</summary>
    public bool Collapsible { get; set; }

    /// <summary>Add a maximize tool.</summary>
    public bool Maximizable { get; set; }

    /// <summary>Add a remove tool.</summary>
    public bool Removable { get; set; }

    /// <summary>Extra classes on the card body.</summary>
    public string? BodyClass { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var enc = HtmlEncoder.Default;
        var body = (await output.GetChildContentAsync()).GetContent();

        output.TagName = "div";
        output.Attributes.SetAttribute("class", $"card {ThemeClass(enc)}".TrimEnd());

        var hasHeader = !string.IsNullOrEmpty(Title) || Collapsible || Maximizable || Removable;
        var sb = new System.Text.StringBuilder();

        if (hasHeader)
        {
            sb.Append("""<div class="card-header">""");
            sb.Append("""<h3 class="card-title">""");
            if (!string.IsNullOrEmpty(Icon))
                sb.Append($"""<i class="{enc.Encode(Icon)} me-2"></i>""");
            sb.Append(enc.Encode(Title ?? string.Empty)).Append("</h3>");
            sb.Append("""<div class="card-tools">""");
            if (Collapsible)
                sb.Append("""<button type="button" class="btn btn-tool" data-lte-toggle="card-collapse" aria-label="Collapse"><i class="bi bi-dash-lg"></i></button>""");
            if (Maximizable)
                sb.Append("""<button type="button" class="btn btn-tool" data-lte-toggle="card-maximize" aria-label="Maximize"><i class="bi bi-fullscreen"></i></button>""");
            if (Removable)
                sb.Append("""<button type="button" class="btn btn-tool" data-lte-toggle="card-remove" aria-label="Remove"><i class="bi bi-x-lg"></i></button>""");
            sb.Append("</div></div>");
        }

        var bodyClass = string.IsNullOrEmpty(BodyClass) ? "card-body" : $"card-body {enc.Encode(BodyClass)}";
        sb.Append($"""<div class="{bodyClass}">""").Append(body).Append("</div>");

        output.Content.SetHtmlContent(sb.ToString());
    }

    private string ThemeClass(HtmlEncoder enc)
    {
        if (string.IsNullOrEmpty(Theme))
            return string.Empty;
        var t = enc.Encode(Theme);
        return Variant switch
        {
            "outline" => $"card-outline card-{t}",
            "solid" => $"text-bg-{t}",
            _ => $"card-{t}",
        };
    }
}
