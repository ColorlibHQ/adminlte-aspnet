using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ColorlibHQ.AdminLTE.AspNetCore.TagHelpers;

/// <summary>
/// Server-rendered AdminLTE description-block — a compact labelled stat with an optional trend.
/// Usage: <c>&lt;adminlte-description-block header="$35,210" text="TOTAL REVENUE" percentage="17" /&gt;</c>.
/// </summary>
[HtmlTargetElement("adminlte-description-block", TagStructure = TagStructure.WithoutEndTag)]
public sealed class DescriptionBlockTagHelper : TagHelper
{
    /// <summary>The big value / header.</summary>
    public string Header { get; set; } = string.Empty;

    /// <summary>Caption under the header.</summary>
    public string? Text { get; set; }

    /// <summary>Optional Bootstrap Icons class.</summary>
    public string? Icon { get; set; }

    /// <summary>Icon color theme.</summary>
    public string? IconTheme { get; set; }

    /// <summary>Trend percentage; positive renders green-up, negative red-down. Use NaN for none.</summary>
    public double Percentage { get; set; } = double.NaN;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var enc = HtmlEncoder.Default;
        var sb = new StringBuilder();

        sb.Append("""<div class="description-block">""");
        if (!double.IsNaN(Percentage))
        {
            var up = Percentage >= 0;
            var abs = Math.Abs(Percentage).ToString(CultureInfo.InvariantCulture);
            sb.Append($"""<span class="description-percentage {(up ? "text-success" : "text-danger")}">""");
            sb.Append($"""<i class="bi {(up ? "bi-caret-up-fill" : "bi-caret-down-fill")}"></i>{abs}%</span>""");
        }
        sb.Append("""<h5 class="description-header">""");
        if (!string.IsNullOrEmpty(Icon))
        {
            var color = string.IsNullOrEmpty(IconTheme) ? "" : $" text-{enc.Encode(IconTheme)}";
            sb.Append($"""<i class="bi {enc.Encode(Icon)}{color} me-1"></i>""");
        }
        sb.Append(enc.Encode(Header)).Append("</h5>");
        if (!string.IsNullOrEmpty(Text))
            sb.Append($"""<span class="description-text">{enc.Encode(Text)}</span>""");
        sb.Append("</div>");

        output.TagName = null;
        output.Content.SetHtmlContent(sb.ToString());
    }
}
