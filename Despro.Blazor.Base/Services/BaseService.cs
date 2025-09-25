using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Despro.Blazor.Base.Services
{
    public class BaseService
    {
        private readonly IJSRuntime _jsRuntime;

        public BaseService(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }

        public async Task SetTheme(bool darkTheme)
        {
            string theme = "";
            if (darkTheme)
            {
                theme = "dark";
            }

            await _jsRuntime.InvokeVoidAsync("DesproBlazor.setTheme", theme);
        }

        public async Task<string> OpenContentWindow(string contentType, byte[] content, string urlSuffix = null, string name = null, string features = null)
        {
            return await _jsRuntime.InvokeAsync<string>("DesproBlazor.openContentWindow", contentType, content, urlSuffix, name, features);
        }

        public async Task<string> CreateObjectURLAsync(string contentType, byte[] content)
        {
            return await _jsRuntime.InvokeAsync<string>("DesproBlazor.createObjectURL", contentType, content);
        }

        public async Task RevokeObjectURLAsync(string objectURL)
        {
            await _jsRuntime.InvokeVoidAsync("DesproBlazor.revokeObjectURL", objectURL);
        }

        public async Task SaveAsBinary(string fileName, string contentType, byte[] content)
        {
            await _jsRuntime.InvokeVoidAsync("DesproBlazor.saveAsBinary", fileName, contentType, content);
        }

        public async Task SaveAsFile(string fileName, string href)
        {
            await _jsRuntime.InvokeVoidAsync("DesproBlazor.saveAsFile", fileName, href);
        }

        public async Task PreventDefaultKey(ElementReference element, string eventName, string[] keys)
        {
            await _jsRuntime.InvokeVoidAsync("DesproBlazor.preventDefaultKey", element, eventName, keys);
        }

        public async Task FocusFirstInTableRow(ElementReference tableRow)
        {
            await _jsRuntime.InvokeVoidAsync("DesproBlazor.focusFirstInTableRow", tableRow, "");
        }

        public async Task NavigateTable(ElementReference tableCell, string key)
        {
            await _jsRuntime.InvokeVoidAsync("DesproBlazor.navigateTable", tableCell, key);
        }

        public async Task ScrollToFragment(string fragmentId)
        {
            await _jsRuntime.InvokeVoidAsync("DesproBlazor.scrollToFragment", fragmentId);
        }

        public async Task ShowAlert(string message)
        {
            await _jsRuntime.InvokeVoidAsync("DesproBlazor.showAlert", message);
        }

        public async Task CopyToClipboard(string text)
        {
            await _jsRuntime.InvokeVoidAsync("DesproBlazor.copyToClipboard", text);
        }

        public async Task<string> ReadFromClipboard()
        {
            return await _jsRuntime.InvokeAsync<string>("DesproBlazor.readFromClipboard");
        }

        public async Task DisableDraggable(ElementReference container, ElementReference element)
        {
            await _jsRuntime.InvokeVoidAsync("DesproBlazor.disableDraggable", container, element);
        }

        public async Task SetElementProperty(ElementReference element, string property, object value)
        {
            await _jsRuntime.InvokeVoidAsync("DesproBlazor.setPropByElement", element, property, value);
        }
    }
}