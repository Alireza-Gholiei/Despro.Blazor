//window.DesproBlazor = {
//    setTheme: function (theme) {
//        document.querySelector("body").setAttribute("data-bs-theme", theme);
//    },

//    getUserAgent: function () {
//        return navigator.userAgent;
//    },

//    openContentWindow(contentType, content, urlSuffix, name, features) {
//        const blob = new Blob([content], { type: contentType });
//        var url = URL.createObjectURL(blob);

//        if (urlSuffix) {
//            url = url + urlSuffix;
//        }

//        var newWindow = window.open(url, name, features);

//        var revoke = () => URL.revokeObjectURL(url);

//        newWindow.addEventListener("unload", revoke);
//        newWindow.addEventListener("beforeunload", revoke);
//    },

//    createObjectURL(contentType, content) {
//        const blob = new Blob([content], { type: contentType });
//        return URL.createObjectURL(blob);
//    },

//    revokeObjectURL(objectURL) {
//        URL.revokeObjectURL(objectURL);
//    },

//    saveAsBinary: function (filename, contentType, content) {
//        const file = new File([content], filename, { type: contentType });
//        const exportUrl = URL.createObjectURL(file);
//        const a = document.createElement("a");
//        document.body.appendChild(a);
//        a.href = exportUrl;
//        a.download = filename;
//        a.target = "_self";
//        a.click();
//        URL.revokeObjectURL(exportUrl);
//    },

//    saveAsFile: function (filename, href) {
//        var link = document.createElement('a');
//        link.download = filename;
//        link.href = href;
//        document.body.appendChild(link);
//        link.click();
//        document.body.removeChild(link);
//    },

//    addResizeObserver: (element, dotNetReference) => {
//        const resizeObserver = new ResizeObserver(onResize);
//        resizeObserver.observe(element);

//        function onResize(entries) {
//            const entry = entries[0];
//            console.log(entry);
//            const result = {
//                contentRect: entry.contentRect,
//            };

//            dotNetReference.invokeMethodAsync("ElementResized", result);
//        }
//    },

//    preventDefaultKey: (element, event, keys) => {
//        element.addEventListener(event, (e) => {
//            if (keys.includes(e.key)) {
//                e.preventDefault();
//            }
//        });
//    },

//    focusFirstInTableRow: (tr) => {
//        var td = tr.cells[0];
//        window.DesproBlazor.navigateTable(td, '');
//    },

//    navigateTable: (td, key) => {
//        var tr = td.closest('tr');
//        if (!tr) {
//            return;
//        }

//        var moveToRow = tr;
//        if (key == 'ArrowUp') {
//            moveToRow = tr.parentNode.rows[tr.rowIndex - 2];
//        } else if (key == 'ArrowDown') {
//            moveToRow = tr.parentNode.rows[tr.rowIndex];
//        }

//        if (!moveToRow) {
//            return;
//        }

//        var pos = td.cellIndex;
//        if (key == 'ArrowLeft') {
//            pos = pos - 1;
//        } else if (key == 'ArrowRight') {
//            pos = pos + 1;
//        } else if (key == '') {
//            key = 'ArrowRight';
//        }

//        var moveToCell = moveToRow.cells[pos];
//        if (!moveToCell) {
//            return;
//        }

//        var focusElement = Array.from(moveToCell.getElementsByTagName("*")).find(x => x.tabIndex >= 0);

//        if (focusElement) {
//            focusElement.focus();
//            if (focusElement.select) {
//                focusElement.select();
//            }
//        } else {
//            window.DesproBlazor.navigateTable(moveToCell, key);
//        }
//    },

//    scrollToFragment: (elementId) => {
//        var element = document.getElementById(elementId);

//        if (element) {
//            element.scrollIntoView({
//                behavior: 'smooth'
//            });
//        }
//    },

//    showPrompt: (message, defaultValue) => {
//        return prompt(message, defaultValue);
//    },

//    showAlert: (message) => {
//        return alert(message);
//    },

//    windowOpen: (url, name, features, replace) => {
//        window.open(url, name, features, replace);
//        return "";
//    },

//    redirect: (url) => {
//        window.open(url);
//        return "";
//    },

//    copyToClipboard: (text) => {
//        navigator.clipboard.writeText(text)
//    },

//    readFromClipboard: () => {
//        return navigator.clipboard.readText();
//    },

//    disableDraggable: (container, element) => {

//        element.addEventListener("mousedown", (e) => {
//            e.stopPropagation();
//            container['draggable'] = false;
//        });

//        element.addEventListener("mouseup", (e) => {
//            container['draggable'] = true;
//        });

//        element.addEventListener("mouseleave", (e) => {
//            container['draggable'] = true;
//        });
//    },

//    setPropByElement: (element, property, value) => {
//        element[property] = value;
//        return "";
//    },

//    clickOutsideHandler: {
//        removeEvent: (elementId) => {
//            if (elementId === undefined || window.clickHandlers === undefined) return;
//            if (!window.clickHandlers.has(elementId)) return;

//            var handler = window.clickHandlers.get(elementId);
//            window.removeEventListener("click", handler);
//            window.clickHandlers.delete(elementId);
//        },
//        addEvent: (elementId, unregisterAfterClick, dotnetHelper) => {
//            window.DesproBlazor.clickOutsideHandler.removeEvent(elementId);

//            if (window.clickHandlers === undefined) {
//                window.clickHandlers = new Map();
//            }
//            var currentTime = (new Date()).getTime();

//            var handler = (e) => {
//                var nowTime = (new Date()).getTime();
//                var diff = Math.abs((nowTime - currentTime) / 1000);

//                if (diff < 0.5) {
//                    return;
//                }

//                currentTime = nowTime;

//                var element = document.getElementById(elementId);
//                if (e != null && element != null) {
//                    if (e.target.isConnected === true && e.target !== element && (!element.contains(e.target))) {
//                        if (unregisterAfterClick) {
//                            window.DesproBlazor.clickOutsideHandler.removeEvent(elementId);
//                        }
//                        dotnetHelper.invokeMethodAsync("InvokeClickOutside");
//                    }
//                }
//            };

//            window.clickHandlers.set(elementId, handler);
//            window.addEventListener("click", handler);
//        }
//    }
//}

(() => {
    const observers = new Map();        // resize observers keyed by element
    const clickHandlers = new Map();    // click outside handlers keyed by id
    const keyHandlers = new Map();      // preventDefaultKey handlers keyed by element

    function safeRevoke(url) {
        try {
            if (url) URL.revokeObjectURL(url);
        } catch (e) {
            console.warn('revokeObjectURL failed', e);
        }
    }

    function openContentWindowInternal(contentType, content, urlSuffix, name, features) {
        const blob = new Blob([content], { type: contentType });
        let url = URL.createObjectURL(blob);
        if (urlSuffix) url = url + urlSuffix;

        const newWindow = window.open(url, name || '_blank', features || '');

        // Best-effort revoke: try on unload and also poll for closed window fallback
        const revoke = () => {
            safeRevoke(url);
            try { clearInterval(poll); } catch { };
            try { newWindow && newWindow.removeEventListener('beforeunload', revoke); } catch { }
        };

        try {
            if (newWindow) {
                newWindow.addEventListener('beforeunload', revoke);
            }
        } catch (e) {
            // some browsers may block attaching events to windows
        }

        // Fallback: poll until window closed then revoke
        const poll = setInterval(() => {
            if (!newWindow || newWindow.closed) {
                revoke();
            }
        }, 500);

        return newWindow;
    }

    const DesproBlazor = {
        setTheme(theme) {
            try {
                document.querySelector('body')?.setAttribute('data-bs-theme', theme);
            } catch (e) { console.warn(e); }
        },

        getUserAgent() {
            return navigator.userAgent;
        },

        openContentWindow(contentType, content, urlSuffix, name, features) {
            return openContentWindowInternal(contentType, content, urlSuffix, name, features);
        },

        createObjectURL(contentType, content) {
            try {
                const blob = new Blob([content], { type: contentType });
                return URL.createObjectURL(blob);
            } catch (e) {
                console.warn('createObjectURL failed', e);
                return null;
            }
        },

        revokeObjectURL(objectURL) {
            safeRevoke(objectURL);
        },

        saveAsBinary(filename, contentType, content) {
            try {
                const file = new File([content], filename, { type: contentType });
                const exportUrl = URL.createObjectURL(file);
                const a = document.createElement('a');
                a.style.display = 'none';
                a.href = exportUrl;
                a.download = filename;
                document.body.appendChild(a);
                a.click();
                document.body.removeChild(a);
                // revoke after short timeout to ensure download started
                setTimeout(() => safeRevoke(exportUrl), 1000);
            } catch (e) {
                console.error('saveAsBinary failed', e);
            }
        },

        saveAsFile(filename, href) {
            try {
                const link = document.createElement('a');
                link.download = filename;
                link.href = href;
                link.style.display = 'none';
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
            } catch (e) {
                console.error('saveAsFile failed', e);
            }
        },

        addResizeObserver(element, dotNetReference, id) {
            // id optional: if provided, allows removing observer later
            if (!element) return null;

            // if already observing this element by id, disconnect first
            if (id && observers.has(id)) {
                const old = observers.get(id);
                try { old.disconnect(); } catch { }
                observers.delete(id);
            }

            const resizeObserver = new ResizeObserver((entries) => {
                const entry = entries[0];
                const result = {
                    contentRect: entry.contentRect
                };
                // send minimal data to .NET
                try { dotNetReference.invokeMethodAsync('ElementResized', result); } catch (e) { console.warn(e); }
            });

            resizeObserver.observe(element);

            if (id) observers.set(id, resizeObserver);

            return true;
        },

        removeResizeObserver(idOrElement) {
            // Accept either id (string) or element
            if (!idOrElement) return;
            if (typeof idOrElement === 'string') {
                const obs = observers.get(idOrElement);
                if (obs) {
                    try { obs.disconnect(); } catch { }
                    observers.delete(idOrElement);
                }
            } else if (idOrElement instanceof Element) {
                // find and remove matching observer
                for (const [key, obs] of observers.entries()) {
                    try {
                        obs.unobserve(idOrElement);
                        // if observer has no targets, disconnect
                        // (ResizeObserver doesn't expose targets, so we safe-disconnect)
                        obs.disconnect();
                        observers.delete(key);
                    } catch (e) { }
                }
            }
        },

        preventDefaultKey(element, eventName, keys, id) {
            if (!element || !eventName || !Array.isArray(keys)) return;

            // remove existing handler if provided id
            if (id && keyHandlers.has(id)) {
                const old = keyHandlers.get(id);
                try { element.removeEventListener(eventName, old); } catch { }
                keyHandlers.delete(id);
            }

            const handler = (e) => {
                try {
                    if (keys.includes(e.key)) {
                        e.preventDefault();
                    }
                } catch (err) { }
            };

            element.addEventListener(eventName, handler);
            if (id) keyHandlers.set(id, handler);
        },

        removePreventDefaultKey(id, element, eventName) {
            if (!id) return;
            const handler = keyHandlers.get(id);
            if (handler && element && eventName) {
                try { element.removeEventListener(eventName, handler); } catch { }
            }
            keyHandlers.delete(id);
        },

        focusFirstInTableRow(tr) {
            if (!tr || !tr.cells || tr.cells.length === 0) return;
            const td = tr.cells[0];
            if (!td) return;
            this.navigateTable(td, '');
        },

        navigateTable(td, key) {
            if (!td) return;
            const tr = td.closest('tr');
            if (!tr) return;

            // use tbody rows if present to normalize indexing (ignore thead)
            const parent = tr.parentNode; // could be TBODY or TABLE
            const rows = parent && parent.rows ? parent.rows : (tr.closest('table')?.rows || []);
            if (!rows || rows.length === 0) return;

            let index = tr.rowIndex;

            if (key === 'ArrowUp') index = index - 1;
            else if (key === 'ArrowDown') index = index + 1;

            const moveToRow = rows[index];
            if (!moveToRow) return;

            let pos = td.cellIndex;
            if (key === 'ArrowLeft') pos = pos - 1;
            else if (key === 'ArrowRight') pos = pos + 1;
            else if (key === '') pos = 0; // default to first cell in target row

            if (pos < 0) pos = 0;

            const moveToCell = moveToRow.cells[pos];
            if (!moveToCell) return;

            const focusElement = Array.from(moveToCell.getElementsByTagName('*')).find(x => x.tabIndex >= 0 && !x.disabled);

            if (focusElement) {
                try { focusElement.focus(); if (typeof focusElement.select === 'function') focusElement.select(); } catch (e) { }
            } else {
                // if no focusable element, try next cell recursively (avoid infinite loops)
                if (moveToCell !== td) this.navigateTable(moveToCell, key);
            }
        },

        scrollToFragment(elementId) {
            try {
                const element = document.getElementById(elementId);
                if (element) element.scrollIntoView({ behavior: 'smooth' });
            } catch (e) { }
        },

        showPrompt(message, defaultValue) {
            return prompt(message, defaultValue);
        },

        showAlert(message) {
            alert(message);
            return true;
        },

        windowOpen(url, name, features, replace) {
            const w = window.open(url, name, features, replace);
            return w ? '' : null;
        },

        redirect(url) {
            window.location.href = url;
            return '';
        },

        async copyToClipboard(text) {
            try {
                await navigator.clipboard.writeText(text);
                return true;
            } catch (e) {
                console.warn('copyToClipboard failed', e);
                return false;
            }
        },

        readFromClipboard() {
            try {
                return navigator.clipboard.readText();
            } catch (e) {
                return Promise.reject(e);
            }
        },

        disableDraggable(container, element) {
            if (!container || !element) return;

            const onDown = (e) => { e.stopPropagation(); container.draggable = false; };
            const onUp = (e) => { container.draggable = true; };
            const onLeave = (e) => { container.draggable = true; };

            element.addEventListener('mousedown', onDown);
            element.addEventListener('mouseup', onUp);
            element.addEventListener('mouseleave', onLeave);

            // return a cleanup function pointer so callers can remove listeners
            return () => {
                try {
                    element.removeEventListener('mousedown', onDown);
                    element.removeEventListener('mouseup', onUp);
                    element.removeEventListener('mouseleave', onLeave);
                } catch (e) { }
            };
        },

        setPropByElement(element, property, value) {
            try { if (element) element[property] = value; } catch (e) { }
            return '';
        },

        clickOutsideHandler: {
            removeEvent(elementId) {
                if (!elementId || !clickHandlers.has(elementId)) return;
                const handler = clickHandlers.get(elementId);
                try { window.removeEventListener('click', handler, true); } catch (e) { }
                clickHandlers.delete(elementId);
            },

            addEvent(elementId, unregisterAfterClick, dotnetHelper) {
                this.removeEvent(elementId);
                if (!elementId || !dotnetHelper) return;

                let currentTime = Date.now();

                const handler = (e) => {
                    const nowTime = Date.now();
                    const diff = Math.abs((nowTime - currentTime) / 1000);
                    if (diff < 0.5) return; // debounce quick clicks
                    currentTime = nowTime;

                    const element = document.getElementById(elementId);
                    if (e && element) {
                        try {
                            if (e.target.isConnected === true && e.target !== element && !element.contains(e.target)) {
                                if (unregisterAfterClick) this.removeEvent(elementId);
                                dotnetHelper.invokeMethodAsync('InvokeClickOutside').catch(err => console.warn(err));
                            }
                        } catch (err) { }
                    }
                };

                clickHandlers.set(elementId, handler);
                window.addEventListener('click', handler, true); // use capture to detect earlier
            }
        }
    };

    // expose to window
    window.DesproBlazor = DesproBlazor;
})();