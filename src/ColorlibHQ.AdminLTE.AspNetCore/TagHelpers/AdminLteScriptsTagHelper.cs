using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ColorlibHQ.AdminLTE.AspNetCore.TagHelpers;

/// <summary>
/// Emits the <c>&lt;script&gt;</c> tags for OverlayScrollbars, Bootstrap (bundle with
/// Popper) and AdminLTE from the package's static web assets. Place just before
/// <c>&lt;/body&gt;</c>: <c>&lt;adminlte-scripts /&gt;</c>.
/// </summary>
[HtmlTargetElement("adminlte-scripts", TagStructure = TagStructure.WithoutEndTag)]
public sealed class AdminLteScriptsTagHelper : TagHelper
{
    private const string Base = "/_content/ColorlibHQ.AdminLTE.AspNetCore";

    /// <summary>Use minified assets (default true).</summary>
    public bool Minified { get; set; } = true;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        var min = Minified ? ".min" : string.Empty;

        output.Content.SetHtmlContent(
            $"""
             <script src="{Base}/vendor/overlayscrollbars/js/overlayscrollbars.browser.es6.min.js"></script>
             <script src="{Base}/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
             <script src="{Base}/js/adminlte{min}.js"></script>
             """);
    }
}
