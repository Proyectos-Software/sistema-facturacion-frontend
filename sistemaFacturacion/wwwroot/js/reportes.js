window.openReportPdf = (base64Pdf) => {
    try {
        const byteCharacters = atob(base64Pdf);
        const byteNumbers = new Array(byteCharacters.length);

        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }

        const byteArray = new Uint8Array(byteNumbers);
        const blob = new Blob([byteArray], { type: "application/pdf" });
        const blobUrl = URL.createObjectURL(blob);

        // Abrir modal con PDF
        const iframe = document.getElementById("pdfFrame");
        iframe.src = blobUrl;

        const modal = document.getElementById("pdfModal");
        modal.showModal();
    } catch (err) {
        console.error("Error mostrando PDF:", err);
    }
};
