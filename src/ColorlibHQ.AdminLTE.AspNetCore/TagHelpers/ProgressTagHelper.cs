using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ColorlibHQ.AdminLTE.AspNetCore.TagHelpers;

/// <summary>
/// Server-rendered Bootstrap progress bar.
/// Usage: <c>&lt;adminlte-progress value="80" theme="success" show-label="true" /&gt;</c>.
/// </summary>
[HtmlTargetElement("adminlte-progress", TagStructure = TagStructure.WithoutEndTag)]
public sealed class ProgressTagHelper : TagHelper
{
    /// <summary>Current value.</summary>
    public double Value { get; set; }

    /// <summary>Maximum value.</summary>
    public double Max { get; set; } = 100;

    /// <summary>Bootstrap theme color.</summary>
    public string Theme { get; set; } = "primary";

    /// <summary>Size variant: <c>sm</c> or <c>lg</c>.</summary>
    public string? Size { get; set; }

    /// <summary>Striped pattern.</summary>
    public bool Striped { get; set; }

    /// <summary>Animated stripes.</summary>
    public bool Animated { get; set; }

    /// <summary>Show the percentage label in the bar.</summary>
    public bool ShowLabel { get; set; }

    /// <summary>Custom CSS height, e.g. <c>1.5rem</c>.</summary>
    public string? Height { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var enc = HtmlEncoder.Default;
        var percent = Max <= 0 ? 0 : (int)Math.Round(Math.Clamp(Value / Max, 0, 1) * 100);
        var sizeClass = Size switch { "sm" => " progress-sm", "lg" => " progress-lg", _ => "" };
        var style = string.IsNullOrEmpty(Height) ? "" : $""" style="height:{enc.Encode(Height)}" """.TrimEnd();

        var sb = new StringBuilder();
        sb.Append($"""<div class="progress{sizeClass}"{style} role="progressbar" aria-valuenow="{percent}" aria-valuemin="0" aria-valuemax="{(int)Max}">""");
        var bar = $"progress-bar bg-{enc.Encode(Theme)}";
        if (Striped) bar += " progress-bar-striped";
        if (Animated) bar += " progress-bar-animated";
        sb.Append($"""<div class="{bar}" style="width:{percent}%">""");
        if (ShowLabel)
            sb.Append($"{percent}%");
        sb.Append("</div></div>");

        output.TagName = null;
        output.Content.SetHtmlContent(sb.ToString());
    }
}
