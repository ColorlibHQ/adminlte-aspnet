// AdminLTE color-mode interop module for Blazor.
// Persists the choice to localStorage["lte-theme"] and writes the resolved
// light/dark value to <html data-bs-theme>. Mirrors the Vue/React ports.

const STORAGE_KEY = 'lte-theme';

function systemPrefersDark() {
  return window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;
}

function resolve(mode) {
  if (mode === 'light' || mode === 'dark') return mode;
  return systemPrefersDark() ? 'dark' : 'light';
}

function apply(mode) {
  document.documentElement.setAttribute('data-bs-theme', resolve(mode));
}

export function get() {
  return localStorage.getItem(STORAGE_KEY) || 'auto';
}

export function set(mode) {
  localStorage.setItem(STORAGE_KEY, mode);
  apply(mode);
}

// Keep "auto" in sync with OS changes for the lifetime of the page.
if (window.matchMedia) {
  window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => {
    if (get() === 'auto') apply('auto');
  });
}
