function updateTimer() {
    const timerElement = document.getElementById("timer");
    if (!timerElement) return;

    const future = Date.parse("Dec 19, 2026 11:30:00");
    const now = new Date();
    const diff = future - now;

    const days = Math.floor(diff / (1000 * 60 * 60 * 24));
    const hours = Math.floor(diff / (1000 * 60 * 60));
    const mins = Math.floor(diff / (1000 * 60));
    const secs = Math.floor(diff / 1000);

    const d = days;
    const h = hours - days * 24;
    const m = mins - hours * 60;
    const s = secs - mins * 60;

    timerElement.innerHTML =
        '<div class="text-center"><div class=""><p class="mb-1 fs-13 fw-medium">Days</p><h4 class="mb-0 fw-semibold">' + d + '</h4></div></div>' +
        '<div class="text-center"><div class=""><p class="mb-1 fs-13 fw-medium">Hours</p><h4 class="mb-0 fw-semibold">' + h + '</h4></div></div>' +
        '<div class="text-center"><div class=""><p class="mb-1 fs-13 fw-medium">Minutes</p><h4 class="mb-0 fw-semibold">' + m + '</h4></div></div>' +
        '<div class="text-center"><div class=""><p class="mb-1 fs-13 fw-medium">Seconds</p><h4 class="mb-0 fw-semibold">' + s + '</h4></div></div>';
}