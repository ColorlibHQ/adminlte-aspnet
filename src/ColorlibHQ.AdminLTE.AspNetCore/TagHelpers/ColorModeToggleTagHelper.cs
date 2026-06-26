using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ColorlibHQ.AdminLTE.AspNetCore.TagHelpers;

/// <summary>
/// Server-rendered light/dark/auto color-mode dropdown for MVC / Razor Pages.
/// Relies on the bundled <c>adminlte-theme.js</c> behaviour (and AdminLTE's
/// own data-bs-theme handling) — works without Blazor. Place inside a navbar
/// <c>&lt;ul class="navbar-nav"&gt;</c>: <c>&lt;adminlte-color-mode-toggle /&gt;</c>.
/// </summary>
[HtmlTargetElement("adminlte-color-mode-toggle", TagStructure = TagStructure.WithoutEndTag)]
public sealed class ColorModeToggleTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        output.Content.SetHtmlContent(
            """
            <li class="nav-item dropdown">
              <button id="bd-theme" class="nav-link" type="button" aria-label="Toggle color scheme"
                      data-bs-toggle="dropdown" aria-expanded="false">
                <i class="bi bi-sun-fill" data-lte-theme-icon="light"></i>
                <i class="bi bi-moon-fill d-none" data-lte-theme-icon="dark"></i>
                <i class="bi bi-circle-half d-none" data-lte-theme-icon="auto"></i>
              </button>
              <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="bd-theme" style="--bs-dropdown-min-width: 8rem;">
                <li><button type="button" class="dropdown-item d-flex align-items-center" data-bs-theme-value="light">
                  <i class="bi bi-sun-fill me-2"></i>Light<i class="bi bi-check-lg ms-auto d-none"></i></button></li>
                <li><button type="button" class="dropdown-item d-flex align-items-center" data-bs-theme-value="dark">
                  <i class="bi bi-moon-fill me-2"></i>Dark<i class="bi bi-check-lg ms-auto d-none"></i></button></li>
                <li><button type="button" class="dropdown-item d-flex align-items-center active" data-bs-theme-value="auto">
                  <i class="bi bi-circle-half me-2"></i>Auto<i class="bi bi-check-lg ms-auto d-none"></i></button></li>
              </ul>
            </li>
            """);
    }
}
