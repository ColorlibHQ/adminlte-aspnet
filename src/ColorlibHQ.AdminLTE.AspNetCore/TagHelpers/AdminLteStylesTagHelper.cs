using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ColorlibHQ.AdminLTE.AspNetCore.TagHelpers;

/// <summary>
/// Emits the <c>&lt;link&gt;</c> tags for Bootstrap, Bootstrap Icons, OverlayScrollbars
/// and AdminLTE from the package's static web assets, plus a blocking inline script
/// that applies the persisted color mode before first paint (no theme flash).
/// Usage in the <c>&lt;head&gt;</c>: <c>&lt;adminlte-styles /&gt;</c>.
/// </summary>
[HtmlTargetElement("adminlte-styles", TagStructure = TagStructure.WithoutEndTag)]
public sealed class AdminLteStylesTagHelper : TagHelper
{
    private const string Base = "/_content/ColorlibHQ.AdminLTE.AspNetCore";

    /// <summary>Use minified assets (default true).</summary>
    public bool Minified { get; set; } = true;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null; // render only the inner content
        var min = Minified ? ".min" : string.Empty;

        output.Content.SetHtmlContent(
            $$"""
             <link rel="stylesheet" href="{{Base}}/vendor/bootstrap/css/bootstrap.min.css">
             <link rel="stylesheet" href="{{Base}}/vendor/bootstrap-icons/font/bootstrap-icons.min.css">
             <link rel="stylesheet" href="{{Base}}/vendor/overlayscrollbars/css/overlayscrollbars.min.css">
             <link rel="stylesheet" href="{{Base}}/css/adminlte{{min}}.css">
             <link rel="stylesheet" href="{{Base}}/css/adminlte-blazor.css">
             <script>
               (function () {
                 try {
                   var m = localStorage.getItem('lte-theme') || 'auto';
                   var dark = m === 'dark' || (m === 'auto' && window.matchMedia &&
                     window.matchMedia('(prefers-color-scheme: dark)').matches);
                   document.documentElement.setAttribute('data-bs-theme', dark ? 'dark' : 'light');
                 } catch (e) {}
               })();
             </script>
             """);
    }
}
