# Changelog

All notable changes to `ColorlibHQ.AdminLTE.AspNetCore` are documented here.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.4.0] - 2026-11-17

### Changed

- **Bundled AdminLTE updated to 4.10.2** (from 4.8.4) and **Bootstrap to 5.3.9**
  (from 5.3.8), with Bootstrap Icons 1.13.2 and OverlayScrollbars 2.17.0. The
  refreshed bundle brings the 4.9 print defaults (the app shell no longer
  prints; `data-lte-print="plain"` opts a page into document-style printing),
  `data-lte-contrast="aa"`, `data-lte-primary` theming and the other fixes from
  AdminLTE 4.9.0 to 4.10.2.
- **The demo draws its charts with Chart.js instead of ApexCharts.** From
  ApexCharts 5.2 the library is no longer MIT-licensed, and the demo is a
  template that gets copied into products. The dashboards now follow the core
  AdminLTE demo: Chart.js 4.6.1 plus the shared theme preset
  (`wwwroot/js/chart-theme.js`), which follows the colour mode live.
- Demo: FullCalendar 6.1.20 to 7.1.5 (new stylesheets, Bootstrap 5 theme
  plugin, `Interaction.Draggable`, `color`/`contrastColor` event options) and
  jsVectorMap 1.5.3 to 1.7.0, matching the core demo. The logo pin moves to
  AdminLTE 4.10.2.

## [0.3.1] - 2026-08-19

### Fixed

- **The NuGet package page showed raw HTML as text.** nuget.org renders a
  Markdown subset and escapes raw HTML, so the GitHub README's centred
  `<p align="center">` hero, the `<div>` ecosystem badge strip and the preview
  `<img>` printed verbatim — 23 lines of markup as visible text — and the
  preview image, referenced relatively, could not resolve at all. The package
  now ships `NUGET.md`: the same content in plain Markdown with absolute URLs,
  so it renders properly on nuget.org while `README.md` keeps its richer layout
  on GitHub.

  0.3.0 was already published to NuGet when this was found, and NuGet versions
  are immutable, so the corrected README ships here. No code differs from 0.3.0.

## [0.3.0] - 2026-08-19

### Changed

- **Bundled AdminLTE updated to 4.8.4** (from 4.0.0). The refreshed
  `wwwroot/css/adminlte{,.min,.rtl.min}.css` and `wwwroot/js/adminlte{,.min}.js`
  bring, among other things: the fix for every page scrolling sideways at phone
  widths, the sidebar height fix when a sidebar filter is present, the restyled
  header search, `ColorMode` shipping inside the bundle, `d-print-none` working
  on the sidebar and app wrapper, `data-lte-print="plain"` for pages that print
  as documents, and the pagination focus-ring fix. The demo's CDN logo pin moves
  with it.

### Added

- This changelog. The repository had none: `RELEASING.md` documents the NuGet
  release steps but never included one, so the 0.2.0 release went out
  undocumented.

## [0.2.0] - 2026-06-29

First public release on NuGet: Blazor components, Tag Helpers and the bundled
AdminLTE 4 static web assets. Tagged retroactively — the release was packed and
pushed without step 4 of `RELEASING.md`, so no tag existed until now.

[Unreleased]: https://github.com/ColorlibHQ/adminlte-aspnet/compare/v0.4.0...HEAD
[0.4.0]: https://github.com/ColorlibHQ/adminlte-aspnet/compare/v0.3.1...v0.4.0
[0.3.1]: https://github.com/ColorlibHQ/adminlte-aspnet/compare/v0.3.0...v0.3.1
[0.3.0]: https://github.com/ColorlibHQ/adminlte-aspnet/compare/v0.2.0...v0.3.0
[0.2.0]: https://github.com/ColorlibHQ/adminlte-aspnet/releases/tag/v0.2.0
