# Releasing `ColorlibHQ.AdminLTE.AspNetCore` to NuGet

This document describes the exact steps to cut a release of the AdminLTE 4
ASP.NET Core package and publish it to [nuget.org](https://www.nuget.org/).

## Prerequisites

- .NET SDK **10.0.x** installed. In this environment the SDK lives at `~/.dotnet`
  and is not on `PATH` by default, so export it first:

  ```bash
  export PATH="$HOME/.dotnet:$PATH"
  dotnet --version   # should print 10.0.301 (or newer 10.0.x)
  ```

- A NuGet API key with push rights to the `ColorlibHQ.AdminLTE.AspNetCore`
  package ID. Create one at <https://www.nuget.org/account/apikeys>.

## 1. Bump the version

The package version is the `<Version>` element in
`src/ColorlibHQ.AdminLTE.AspNetCore/ColorlibHQ.AdminLTE.AspNetCore.csproj`.
Update it to the new release version (the port family currently tracks `0.2.0`):

```xml
<Version>0.2.0</Version>
```

Also update `<PackageReleaseNotes>` in the same file if appropriate.

## 2. Build (Release)

```bash
export PATH="$HOME/.dotnet:$PATH"
dotnet build -c Release
```

Confirm **0 errors**.

## 3. Pack

```bash
dotnet pack src/ColorlibHQ.AdminLTE.AspNetCore -c Release
```

This produces both packages under
`src/ColorlibHQ.AdminLTE.AspNetCore/bin/Release/`:

- `ColorlibHQ.AdminLTE.AspNetCore.<version>.nupkg` (main package)
- `ColorlibHQ.AdminLTE.AspNetCore.<version>.snupkg` (symbols)

Verify there are **no pack warnings** and that the package contains the
compiled DLL, `README.md`, and the AdminLTE/Bootstrap `staticwebassets/`:

```bash
unzip -l src/ColorlibHQ.AdminLTE.AspNetCore/bin/Release/ColorlibHQ.AdminLTE.AspNetCore.<version>.nupkg
```

## 4. Tag the release

```bash
git tag v0.2.0
git push origin v0.2.0
```

(Match the tag to the `<Version>` you set in step 1.)

## 5. Push to NuGet

```bash
dotnet nuget push \
  src/ColorlibHQ.AdminLTE.AspNetCore/bin/Release/ColorlibHQ.AdminLTE.AspNetCore.0.2.0.nupkg \
  --api-key <KEY> \
  --source https://api.nuget.org/v3/index.json
```

The matching `.snupkg` symbol package is pushed automatically alongside the
`.nupkg` when both sit in the same directory. To push it explicitly:

```bash
dotnet nuget push \
  src/ColorlibHQ.AdminLTE.AspNetCore/bin/Release/ColorlibHQ.AdminLTE.AspNetCore.0.2.0.snupkg \
  --api-key <KEY> \
  --source https://api.nuget.org/v3/index.json
```

Replace `<KEY>` with your nuget.org API key and `0.2.0` with the released
version. Do **not** commit the key.

## Notes

- `dotnet nuget push` is irreversible — a published version cannot be replaced,
  only unlisted. Double-check the version and package contents before pushing.
- CI (`.github/workflows/ci.yml`) runs restore → build → pack on every push to
  `main` and on pull requests, so a green CI run is a good gate before tagging.
