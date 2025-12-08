let registryLoaded = false;

export async function mountParticlesBG(elementId = "login-bg", options = {}) {

    if (typeof window.tsParticles === 'undefined') {
        console.error('[login-bg] ❌ tsParticles no está disponible globalmente');
        return null;
    }

    const tsParticles = window.tsParticles;

    if (!registryLoaded) {
        console.log('[login-bg] 📦 Usando tsParticles del bundle global');
        registryLoaded = true;
    }

    const element = document.getElementById(elementId);
    if (!element) {
        console.error('[login-bg] ❌ Elemento no encontrado:', elementId);
        return null;
    }
    const base = {
        fullScreen: { enable: false },
        background: { color: "transparent" },
        detectRetina: true,
        fpsLimit: 120,
        pauseOnBlur: true,
        pauseOnOutsideViewport: true,
        particles: {
            number: {
                value: 80,
                density: { enable: true, area: 800 }
            },
            color: {
                value: ["#8b5cf6", "#6d28d9", "#3b82f6", "#2563eb", "#9333ea"]
            },
            shape: {
                type: "circle"
            },
            size: {
                value: { min: 1, max: 5 },
                animation: {
                    enable: true,
                    speed: 4,
                    minimumValue: 0.5,
                    sync: false
                }
            },
            opacity: {
                value: { min: 0.3, max: 0.9 },
                animation: {
                    enable: true,
                    speed: 1.5,
                    minimumValue: 0.3,
                    sync: false
                }
            },
            move: {
                enable: true,
                speed: 1.5,
                direction: "none",
                random: true,
                straight: false,
                outModes: { default: "bounce" }
            },
            links: {
                enable: true,
                distance: 130,
                color: "#93c5fd", 
                opacity: 0.25,
                width: 1
            }
        },
        interactivity: {
            events: {
                onHover: {
                    enable: true,
                    mode: ["repulse", "grab"]
                },
                onClick: {
                    enable: true,
                    mode: "push"
                },
                resize: { enable: true }
            },
            modes: {
                grab: {
                    distance: 160,
                    links: {
                        opacity: 0.8,
                        color: "#a78bfa"
                    }
                },
                repulse: {
                    distance: 120,
                    duration: 0.4
                },
                push: {
                    quantity: 3
                }
            }
        },
        responsive: [
            {
                maxWidth: 992,
                options: {
                    particles: {
                        number: { value: 60 },
                        move: { speed: 1.2 }
                    }
                }
            },
            {
                maxWidth: 576,
                options: {
                    particles: {
                        number: { value: 40 },
                        move: { speed: 1 },
                        links: { distance: 110 }
                    },
                    interactivity: {
                        modes: {
                            grab: { distance: 120 },
                            repulse: { distance: 100 }
                        }
                    }
                }
            }
        ]
    };

    const cfg = { ...base, ...options };

    try {

        const result = await tsParticles.load(elementId, cfg);

        return result;
    } catch (err) {
        console.error('[login-bg] ❌ Error al cargar partículas:', err);
        return null;
    }
}

export function unmountParticlesBG(elementId = "login-bg") {

    if (typeof window.tsParticles === 'undefined') {
        console.warn('[login-bg] ⚠️ tsParticles no disponible');
        return;
    }

    const inst = window.tsParticles.dom().find(c => c.id === elementId);

    if (inst) {
        inst.destroy();
    } else {
        console.log('[login-bg] ⚠️ No se encontró instancia para:', elementId);
    }
}

export function isParticlesMounted(elementId = "login-bg") {
    if (typeof window.tsParticles === 'undefined') return false;
    return window.tsParticles.dom().some(c => c.id === elementId);
}