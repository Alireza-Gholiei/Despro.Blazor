using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Despro.Blazor.Base.Services
{
    public class BaseService(IJSRuntime jSRuntime)
    {
        public async Task SetTheme(bool darkTheme)
        {
            string theme = "";
            if (darkTheme)
            {
                theme = "dark";
            }

            await jSRuntime.InvokeVoidAsync("DesproBlazor.setTheme", theme);
        }

        public async Task<string> OpenContentWindow(string contentType, byte[] content, string urlSuffix = null, string name = null, string features = null)
        {
            return await jSRuntime.InvokeAsync<string>("DesproBlazor.openContentWindow", contentType, content, urlSuffix, name, features);
        }

        public async Task<string> CreateObjectURLAsync(string contentType, byte[] content)
        {
            return await jSRuntime.InvokeAsync<string>("DesproBlazor.createObjectURL", contentType, content);
        }

        public async Task RevokeObjectURLAsync(string objectURL)
        {
            await jSRuntime.InvokeVoidAsync("DesproBlazor.revokeObjectURL", objectURL);
        }

        public async Task SaveAsBinary(string fileName, string contentType, byte[] content)
        {
            await jSRuntime.InvokeVoidAsync("DesproBlazor.saveAsBinary", fileName, contentType, content);
        }

        public async Task SaveAsFile(string fileName, string href)
        {
            await jSRuntime.InvokeVoidAsync("DesproBlazor.saveAsFile", fileName, href);
        }

        public async Task PreventDefaultKey(ElementReference element, string eventName, string[] keys)
        {
            await jSRuntime.InvokeVoidAsync("DesproBlazor.preventDefaultKey", element, eventName, keys);
        }

        public async Task FocusFirstInTableRow(ElementReference tableRow)
        {
            await jSRuntime.InvokeVoidAsync("DesproBlazor.focusFirstInTableRow", tableRow, "");
        }

        public async Task NavigateTable(ElementReference tableCell, string key)
        {
            await jSRuntime.InvokeVoidAsync("DesproBlazor.navigateTable", tableCell, key);
        }

        public async Task ScrollToFragment(string fragmentId)
        {
            await jSRuntime.InvokeVoidAsync("DesproBlazor.scrollToFragment", fragmentId);
        }

        public async Task ShowAlert(string message)
        {
            await jSRuntime.InvokeVoidAsync("DesproBlazor.showAlert", message);
        }

        public async Task CopyToClipboard(string text)
        {
            await jSRuntime.InvokeVoidAsync("DesproBlazor.copyToClipboard", text);
        }

        public async Task<string> ReadFromClipboard()
        {
            return await jSRuntime.InvokeAsync<string>("DesproBlazor.readFromClipboard");
        }

        public async Task DisableDraggable(ElementReference container, ElementReference element)
        {
            await jSRuntime.InvokeVoidAsync("DesproBlazor.disableDraggable", container, element);
        }

        public async Task SetElementProperty(ElementReference element, string property, object value)
        {
            await jSRuntime.InvokeVoidAsync("DesproBlazor.setPropByElement", element, property, value);
        }
    }
}