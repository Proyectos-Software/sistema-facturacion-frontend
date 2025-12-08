// wwwroot/js/interop.js
window.interop = (function () {
    const html = document.documentElement;
    const listeners = { nav: null, header: null, scroll: null };

    // -------- Atributos en <html> --------
    function addAttributeToHtml(name, value) { html.setAttribute(name, value ?? ""); return "ok"; }
    function removeAttributeFromHtml(name) { html.removeAttribute(name); return "ok"; }
    function getAttributeToHtml(name) { return html.getAttribute(name); }

    // -------- CSS Variables (:root) --------
    function setCssVariable(k, v) { html.style.setProperty(k, v); return "ok"; }
    function removeCssVariable(k) { html.style.removeProperty(k); return "ok"; }
    function setclearCssVariables() {
        ['--body-bg-rgb', '--body-bg-rgb2', '--light-rgb', '--form-control-bg', '--input-border', '--gray-3', '--primary-rgb']
            .forEach(k => html.style.removeProperty(k));
        return "ok";
    }

    // -------- LocalStorage helpers --------
    function clearAllLocalStorage() { localStorage.clear(); return "ok"; }
    function setLocalStorageItem(k, v) { localStorage.setItem(k, v ?? ""); return "ok"; }
    function getLocalStorageItem(k) { return localStorage.getItem(k); }
    function removeLocalStorageItem(k) { localStorage.removeItem(k); return "ok"; }
    function setLocalStorageJSON(k, obj) { localStorage.setItem(k, JSON.stringify(obj ?? {})); return "ok"; }
    function getLocalStorageJSON(k) {
        const raw = localStorage.getItem(k);
        if (!raw) return null;
        try { return JSON.parse(raw); } catch { return null; }
    }

    // -------- Utilidades --------
    function inner(prop) { return window[prop] ?? 0; }
    function isEleExist(sel) { return !!document.querySelector(sel); }
    function applyPageStyleClass() { return "ok"; }
    function scrollToTop() { window.scrollTo({ top: 0, behavior: 'smooth' }); return "ok"; }

    // Tooltips Bootstrap (si existe)
    function initializeTooltips() {
        try {
            if (window.bootstrap && typeof window.bootstrap.Tooltip === "function") {
                [...document.querySelectorAll('[data-bs-toggle="tooltip"]')]
                    .forEach(el => new window.bootstrap.Tooltip(el));
            }
            return "ok";
        } catch { return "err"; }
    }

    // Visibilidad por scroll (para .NET)
    function updateScrollVisibility(dotnetRef) {
        const handler = () => dotnetRef.invokeMethodAsync('UpdateScrollVisibility', Math.floor(window.scrollY || 0));
        window.addEventListener('scroll', handler, { passive: true });
        handler();
        listeners.scroll = handler;
        return "ok";
    }
    function unregisterUpdateScrollVisibility() {
        if (listeners.scroll) {
            window.removeEventListener('scroll', listeners.scroll);
            listeners.scroll = null;
        }
        return "ok";
    }

    // Sticky del NavMenu
    function registerScrollListener(dotnetHelper) {
        const handler = () => dotnetHelper.invokeMethodAsync("SetStickyClass", Math.floor(window.scrollY || 0));
        window.addEventListener('scroll', handler, { passive: true });
        handler();
        listeners.nav = handler;
        return "ok";
    }
    function unregisterScrollListener() {
        if (listeners.nav) {
            window.removeEventListener('scroll', listeners.nav);
            listeners.nav = null;
        }
        return "ok";
    }

    // Sticky del Header
    function registerheaderScrollListener(dotnetHelper) {
        const handler = () => dotnetHelper.invokeMethodAsync("SetStickyClass1", Math.floor(window.scrollY || 0));
        window.addEventListener('scroll', handler, { passive: true });
        handler();
        listeners.header = handler;
        return "ok";
    }
    function unregisterHeaderScrollListener() {
        if (listeners.header) {
            window.removeEventListener('scroll', listeners.header);
            listeners.header = null;
        }
        return "ok";
    }

    // API pública
    return {
        // ya tenías
        addAttributeToHtml, removeAttributeFromHtml, getAttributeToHtml,
        setCssVariable, removeCssVariable,
        clearAllLocalStorage, setLocalStorageItem, getLocalStorageItem,
        inner, isEleExist, applyPageStyleClass,
        // nuevas y corregidas
        removeLocalStorageItem, setclearCssVariables,
        initializeTooltips, scrollToTop,
        updateScrollVisibility, unregisterUpdateScrollVisibility,
        registerScrollListener, unregisterScrollListener,
        registerheaderScrollListener, unregisterHeaderScrollListener,
        setLocalStorageJSON, getLocalStorageJSON
    };
})();
