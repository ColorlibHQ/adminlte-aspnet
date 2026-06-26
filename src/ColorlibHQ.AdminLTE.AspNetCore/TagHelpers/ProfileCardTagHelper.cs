using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ColorlibHQ.AdminLTE.AspNetCore.TagHelpers;

/// <summary>
/// Server-rendered AdminLTE box-profile user card.
/// Usage: <c>&lt;adminlte-profile-card name="Jane Doe" title="Developer" image="/u/jane.jpg" stats="Model.Stats" /&gt;</c>.
/// </summary>
[HtmlTargetElement("adminlte-profile-card")]
public sealed class ProfileCardTagHelper : TagHelper
{
    /// <summary>A profile statistic.</summary>
    public sealed record Stat(string Label, string Value);

    /// <summary>A social link.</summary>
    public sealed record Social(string Icon, string Url, string? Color = null);

    /// <summary>User display name.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Job title / role.</summary>
    public string? Title { get; set; }

    /// <summary>Avatar image URL.</summary>
    public string? Image { get; set; }

    /// <summary>Avatar alt text (defaults to <see cref="Name"/>).</summary>
    public string? ImageAlt { get; set; }

    /// <summary>Profile statistics list.</summary>
    public IReadOnlyList<Stat>? Stats { get; set; }

    /// <summary>Social links.</summary>
    public IReadOnlyList<Social>? Socials { get; set; }

    /// <summary>Extra classes on the card.</summary>
    public string? Class { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var enc = HtmlEncoder.Default;
        var body = (await output.GetChildContentAsync()).GetContent();
        var sb = new StringBuilder();
        var extra = string.IsNullOrEmpty(Class) ? "" : $" {enc.Encode(Class)}";

        sb.Append($"""<div class="card card-primary card-outline{extra}"><div class="card-body box-profile">""");
        sb.Append("""<div class="text-center">""");
        if (!string.IsNullOrEmpty(Image))
            sb.Append($"""<img src="{enc.Encode(Image)}" alt="{enc.Encode(ImageAlt ?? Name)}" class="profile-user-img img-fluid img-circle rounded-circle shadow" width="100" height="100" />""");
        sb.Append("</div>");
        sb.Append($"""<h3 class="profile-username text-center">{enc.Encode(Name)}</h3>""");
        if (!string.IsNullOrEmpty(Title))
            sb.Append($"""<p class="text-body-secondary text-center">{enc.Encode(Title)}</p>""");
        if (Stats is { Count: > 0 })
        {
            sb.Append("""<ul class="list-group list-group-unbordered mb-3">""");
            foreach (var stat in Stats)
                sb.Append($"""<li class="list-group-item"><b>{enc.Encode(stat.Label)}</b> <span class="float-end">{enc.Encode(stat.Value)}</span></li>""");
            sb.Append("</ul>");
        }
        if (Socials is { Count: > 0 })
        {
            sb.Append("""<div class="text-center mb-3">""");
            foreach (var social in Socials)
                sb.Append($"""<a href="{enc.Encode(SafeHref(social.Url))}" class="btn btn-outline-{enc.Encode(social.Color ?? "secondary")} btn-sm me-1" target="_blank" rel="noopener noreferrer" aria-label="{enc.Encode(social.Icon)}"><i class="bi {enc.Encode(social.Icon)}"></i></a>""");
            sb.Append("</div>");
        }
        sb.Append(body).Append("</div></div>");

        output.TagName = null;
        output.Content.SetHtmlContent(sb.ToString());
    }

    // Reject dangerous URL schemes (mirrors the Laravel port and the Blazor ProfileCard).
    private static string SafeHref(string url)
    {
        var trimmed = url.TrimStart();
        return trimmed.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase)
            || trimmed.StartsWith("data:", StringComparison.OrdinalIgnoreCase)
            || trimmed.StartsWith("vbscript:", StringComparison.OrdinalIgnoreCase)
            ? "#"
            : url;
    }
}
