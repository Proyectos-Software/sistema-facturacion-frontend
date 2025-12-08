let registryLoaded = false;


export async function mountParticlesBG(elementId = "particles-js", options = {}) {


    if (!registryLoaded) {
        await loadSlim(tsParticles);
        registryLoaded = true;
    }

    const base = {
        fullScreen: { enable: false },
        background: { color: "transparent" },
        detectRetina: true,
        fpsLimit: 60,
        particles: {
            number: {
                value: 80,
                density: { enable: true, area: 800 }
            },
            color: {
                value: "#6366f1"
            },
            size: {
                value: { min: 2, max: 4 } 
            },
            opacity: {
                value: 0.8
            },
            move: {
                enable: true,
                speed: 1,
                outModes: { default: "out" }
            },
            links: {
                enable: true,
                distance: 150,
                color: "#6366f1",
                opacity: 0.4,
                width: 1
            }
        },
        interactivity: {
            events: {
                onHover: { enable: true, mode: "grab" },
                resize: true
            },
            modes: {
                grab: {
                    distance: 140,
                    links: { opacity: 0.8 }
                }
            }
        }
    };

    const cfg = { ...base, ...options };

    console.log('Cargando partículas en:', elementId);

    const result = await tsParticles.load(elementId, cfg);
    console.log('Partículas cargadas:', result);

    return result;
}

export function unmountParticlesBG(elementId = "particles-js") {

    if (typeof window.tsParticles === 'undefined') return;

    const inst = window.tsParticles.dom().find(c => c.id === elementId);
    if (inst) {
        console.log('Destruyendo partículas');
        inst.destroy();
    }
}