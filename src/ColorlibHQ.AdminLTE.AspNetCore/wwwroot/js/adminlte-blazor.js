// AdminLTE <-> Blazor glue: (re)initialise the AdminLTE plugins (PushMenu,
// Treeview, CardWidget, …) and OverlayScrollbars after Blazor renders the DOM,
// plus a ⌘K hook the command palette component subscribes to.

export function initAdminLte() {
  // AdminLTE's bundle auto-initialises on DOMContentLoaded. For interactive
  // Blazor renders the markup arrives later, so re-run the initialiser if the
  // bundle exposed one. The UMD build attaches to window as `adminlte`.
  const lte = window.adminlte;
  if (lte) {
    // Re-bind treeview + push-menu toggles to freshly rendered nodes.
    try { lte.Treeview?.init?.(); } catch { /* no-op */ }
    try { lte.PushMenu?.init?.(); } catch { /* no-op */ }
    try { lte.CardWidget?.init?.(); } catch { /* no-op */ }
  }
  initSidebarScroll();
}

function initSidebarScroll() {
  const wrapper = document.querySelector('.sidebar-wrapper');
  if (!wrapper || wrapper.dataset.osInitialised) return;
  const OS = window.OverlayScrollbarsGlobal?.OverlayScrollbars || window.OverlayScrollbars;
  if (!OS) return;
  OS(wrapper, {
    scrollbars: { theme: 'os-theme-light', autoHide: 'leave', clickScroll: true },
  });
  wrapper.dataset.osInitialised = 'true';
}

let paletteRef = null;

// Registers the ⌘K / Ctrl+K shortcut. `dotnetRef` must expose `OpenPalette`.
export function registerCommandPalette(dotnetRef) {
  paletteRef = dotnetRef;
  document.addEventListener('keydown', onKeydown);
}

export function unregisterCommandPalette() {
  paletteRef = null;
  document.removeEventListener('keydown', onKeydown);
}

function onKeydown(e) {
  if ((e.metaKey || e.ctrlKey) && e.key.toLowerCase() === 'k') {
    e.preventDefault();
    paletteRef?.invokeMethodAsync('OpenPalette');
  }
}

export function focusElement(el) {
  el?.focus?.();
}
