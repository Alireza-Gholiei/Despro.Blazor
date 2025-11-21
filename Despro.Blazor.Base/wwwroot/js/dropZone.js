export function initializeFileDropZone(dropZoneElement, inputFile) {

    if (!dropZoneElement || !inputFile) {
        console.warn("DropZone: element or inputFile is null, initialization skipped.");
        return {
            dispose: () => { /* no-op */ }
        };
    }

    function onDragHover(e) {
        e.preventDefault();
        dropZoneElement.classList.add("hover");
    }

    function onDragLeave(e) {
        e.preventDefault();
        dropZoneElement.classList.remove("hover");
    }

    function onDrop(e) {
        e.preventDefault();
        dropZoneElement.classList.remove("hover");
        inputFile.files = e.dataTransfer.files;
        const event = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(event);
    }

    function onPaste(e) {
        inputFile.files = e.clipboardData.files;
        const event = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(event);
    }

    dropZoneElement.addEventListener("dragenter", onDragHover);
    dropZoneElement.addEventListener("dragover", onDragHover);
    dropZoneElement.addEventListener("dragleave", onDragLeave);
    dropZoneElement.addEventListener("drop", onDrop);
    dropZoneElement.addEventListener('paste', onPaste);

    return {
        dispose: () => {
            dropZoneElement.removeEventListener('dragenter', onDragHover);
            dropZoneElement.removeEventListener('dragover', onDragHover);
            dropZoneElement.removeEventListener('dragleave', onDragLeave);
            dropZoneElement.removeEventListener("drop", onDrop);
            dropZoneElement.removeEventListener('paste', onPaste);
        }
    }
}

export function setFilePreview(inputElement) {
    const file = inputElement.files[0];
    if (file) {
        const url = URL.createObjectURL(file);
        // پیدا کردن video/img نزدیک
        const container = inputElement.closest('.drop-zone');
        const media = container.querySelector('video, img');
        if (media) media.src = url;
        return url;
    }
    return null;
}

export function revokeObjectUrl(url) {
    if (url) URL.revokeObjectURL(url);
}