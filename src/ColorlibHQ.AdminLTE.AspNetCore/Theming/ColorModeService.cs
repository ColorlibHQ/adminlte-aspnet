using Microsoft.JSInterop;

namespace ColorlibHQ.AdminLTE.AspNetCore.Theming;

/// <summary>
/// Manages the active color mode for the current circuit. The mode is persisted
/// to <c>localStorage["lte-theme"]</c> and written to <c>data-bs-theme</c> on the
/// document via the bundled <c>adminlte-theme.js</c> interop module. A blocking
/// inline script (rendered by the layout) sets the attribute before first paint
/// to avoid a flash of the wrong theme.
/// </summary>
public sealed class ColorModeService(IJSRuntime js) : IAsyncDisposable
{
    private IJSObjectReference? _module;

    /// <summary>The current selection (auto/light/dark). Defaults to <see cref="ColorMode.Auto"/>.</summary>
    public ColorMode Current { get; private set; } = ColorMode.Auto;

    /// <summary>Raised after the mode changes so components (e.g. the toggle) can re-render.</summary>
    public event Action? Changed;

    /// <summary>
    /// Reads any persisted preference from localStorage. Safe to call only after the
    /// first interactive render (JS interop is unavailable during prerender).
    /// </summary>
    public async ValueTask InitializeAsync()
    {
        var module = await GetModuleAsync();
        var token = await module.InvokeAsync<string?>("get");
        Current = ColorModeExtensions.Parse(token);
        Changed?.Invoke();
    }

    /// <summary>Sets, applies and persists a new color mode.</summary>
    public async ValueTask SetAsync(ColorMode mode)
    {
        Current = mode;
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("set", mode.ToToken());
        Changed?.Invoke();
    }

    private async ValueTask<IJSObjectReference> GetModuleAsync() =>
        _module ??= await js.InvokeAsync<IJSObjectReference>(
            "import", "./_content/ColorlibHQ.AdminLTE.AspNetCore/js/adminlte-theme.js");

    public async ValueTask DisposeAsync()
    {
        if (_module is not null)
        {
            try
            {
                await _module.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
                // Circuit already gone; nothing to dispose.
            }
        }
    }
}
