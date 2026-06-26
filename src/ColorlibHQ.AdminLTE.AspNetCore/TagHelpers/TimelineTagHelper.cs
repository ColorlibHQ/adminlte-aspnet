using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ColorlibHQ.AdminLTE.AspNetCore.TagHelpers;

/// <summary>
/// Server-rendered AdminLTE timeline. Pass the entries from view code:
/// <c>&lt;adminlte-timeline items="Model.Events" /&gt;</c>.
/// </summary>
[HtmlTargetElement("adminlte-timeline", TagStructure = TagStructure.WithoutEndTag)]
public sealed class TimelineTagHelper : TagHelper
{
    /// <summary>One timeline entry.</summary>
    public sealed class Entry
    {
        public string? Time { get; set; }
        public string? Icon { get; set; }
        public string? IconTheme { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string? Body { get; set; }
        public string? Footer { get; set; }
    }

    /// <summary>The timeline entries, newest first.</summary>
    public IReadOnlyList<Entry> Items { get; set; } = Array.Empty<Entry>();

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var enc = HtmlEncoder.Default;
        var sb = new StringBuilder();

        sb.Append("""<div class="timeline">""");
        foreach (var item in Items)
        {
            var icon = string.IsNullOrEmpty(item.Icon) ? "bi-clock" : item.Icon;
            sb.Append($"""<div><i class="bi {enc.Encode(icon)} bg-{enc.Encode(item.IconTheme ?? "primary")}"></i><div class="timeline-item">""");
            if (!string.IsNullOrEmpty(item.Time))
                sb.Append($"""<span class="time"><i class="bi bi-clock"></i> {enc.Encode(item.Time)}</span>""");
            sb.Append("""<h3 class="timeline-header">""");
            if (!string.IsNullOrEmpty(item.Url))
                sb.Append($"""<a href="{enc.Encode(item.Url)}">{enc.Encode(item.Title)}</a>""");
            else
                sb.Append(enc.Encode(item.Title));
            sb.Append("</h3>");
            if (!string.IsNullOrEmpty(item.Body))
                sb.Append($"""<div class="timeline-body">{enc.Encode(item.Body)}</div>""");
            if (!string.IsNullOrEmpty(item.Footer))
                sb.Append($"""<div class="timeline-footer">{enc.Encode(item.Footer)}</div>""");
            sb.Append("</div></div>");
        }
        sb.Append("</div>");

        output.TagName = null;
        output.Content.SetHtmlContent(sb.ToString());
    }
}
