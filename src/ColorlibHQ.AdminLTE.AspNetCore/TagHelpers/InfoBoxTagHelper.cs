using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ColorlibHQ.AdminLTE.AspNetCore.TagHelpers;

/// <summary>
/// Server-rendered AdminLTE info-box stat widget.
/// Usage: <c>&lt;adminlte-info-box text="Messages" title="1,410" icon="bi-envelope-fill" theme="info" /&gt;</c>.
/// </summary>
[HtmlTargetElement("adminlte-info-box", TagStructure = TagStructure.WithoutEndTag)]
public sealed class InfoBoxTagHelper : TagHelper
{
    /// <summary>The number / stat (right column).</summary>
    public string? Title { get; set; }

    /// <summary>Label (left column).</summary>
    public string? Text { get; set; }

    /// <summary>Bootstrap Icons class.</summary>
    public string? Icon { get; set; }

    /// <summary>Bootstrap theme for the icon chip / solid background.</summary>
    public string Theme { get; set; } = "info";

    /// <summary>Override the icon color independently of <see cref="Theme"/>.</summary>
    public string? IconTheme { get; set; }

    /// <summary><c>default</c> or <c>solid</c> (fully colored box).</summary>
    public string Variant { get; set; } = "default";

    /// <summary>Unit suffix appended to the number (e.g. <c>%</c>).</summary>
    public string? Unit { get; set; }

    /// <summary>Progress bar value (0-100). When negative, no bar is shown.</summary>
    public int Progress { get; set; } = -1;

    /// <summary>Caption under the progress bar.</summary>
    public string? ProgressText { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var enc = HtmlEncoder.Default;
        var sb = new StringBuilder();
        var solid = Variant == "solid";

        sb.Append($"""<div class="info-box{(solid ? $" text-bg-{enc.Encode(Theme)}" : "")}">""");
        if (!string.IsNullOrEmpty(Icon))
        {
            var iconChip = solid ? "" : $" text-bg-{enc.Encode(IconTheme ?? Theme)}";
            sb.Append($"""<span class="info-box-icon{iconChip}"><i class="bi {enc.Encode(Icon)}"></i></span>""");
        }
        sb.Append("""<div class="info-box-content">""");
        if (!string.IsNullOrEmpty(Text))
            sb.Append($"""<span class="info-box-text">{enc.Encode(Text)}</span>""");
        if (!string.IsNullOrEmpty(Title))
        {
            sb.Append($"""<span class="info-box-number">{enc.Encode(Title)}""");
            if (!string.IsNullOrEmpty(Unit))
                sb.Append($"<small>{enc.Encode(Unit)}</small>");
            sb.Append("</span>");
        }
        if (Progress >= 0)
        {
            var pct = Math.Clamp(Progress, 0, 100);
            sb.Append($"""<div class="progress"><div class="progress-bar bg-{enc.Encode(Theme)}" style="width:{pct}%"></div></div>""");
        }
        if (!string.IsNullOrEmpty(ProgressText))
            sb.Append($"""<span class="progress-description">{enc.Encode(ProgressText)}</span>""");
        sb.Append("</div></div>");

        output.TagName = null;
        output.Content.SetHtmlContent(sb.ToString());
    }
}
