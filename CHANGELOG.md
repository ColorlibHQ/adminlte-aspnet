# Changelog

All notable changes to `ColorlibHQ.AdminLTE.AspNetCore` are documented here.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

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

[Unreleased]: https://github.com/ColorlibHQ/adminlte-aspnet/compare/v0.3.0...HEAD
[0.3.0]: https://github.com/ColorlibHQ/adminlte-aspnet/compare/v0.2.0...v0.3.0
[0.2.0]: https://github.com/ColorlibHQ/adminlte-aspnet/releases/tag/v0.2.0
