# Changelog

All notable changes to `ColorlibHQ.AdminLTE.AspNetCore` are documented here.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

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

[Unreleased]: https://github.com/ColorlibHQ/adminlte-aspnet/compare/v0.3.1...HEAD
[0.3.1]: https://github.com/ColorlibHQ/adminlte-aspnet/compare/v0.3.0...v0.3.1
[0.3.0]: https://github.com/ColorlibHQ/adminlte-aspnet/compare/v0.2.0...v0.3.0
[0.2.0]: https://github.com/ColorlibHQ/adminlte-aspnet/releases/tag/v0.2.0
