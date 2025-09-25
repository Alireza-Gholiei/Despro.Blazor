using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Despro.Blazor.Form.Components.DragDrop
{
    public partial class DragDrop : BaseComponent, IAsyncDisposable
    {
        [Inject] private IJSRuntime JsRuntime { get; set; }

        [Parameter] public EventCallback<IBrowserFile> SelectedFileChanged { get; set; }
        [Parameter] public IBrowserFile SelectedFile { get; set; }
        [Parameter] public string Label { get; set; }
        [Parameter] public string? Src { get; set; }

        private ElementReference _dropZoneElement;
        private InputFile _inputFile;

        private IJSObjectReference _module;
        private IJSObjectReference _dropZoneInstance;
        private bool _isFile = false;
        private string _src;

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Src))
            {
                await using Stream stream = File.Open(Src, FileMode.OpenOrCreate);
                using MemoryStream ms = new();
                await stream.CopyToAsync(ms);

                _src = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());

                _isFile = true;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Despro.Blazor.Base/js/dropZone.min.js");

                    _dropZoneInstance = await _module.InvokeAsync<IJSObjectReference>("initializeFileDropZone", _dropZoneElement, _inputFile.Element);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private async Task OnChange(InputFileChangeEventArgs e)
        {
            try
            {
                SelectedFile = e.File;
                await using Stream stream = e.File.OpenReadStream(StaticValues.ImageSize);
                using MemoryStream ms = new();
                await stream.CopyToAsync(ms);
                _src = "data:" + e.File.ContentType + ";base64," + Convert.ToBase64String(ms.ToArray());
                _isFile = true;
                await SelectedFileChanged.InvokeAsync(e.File);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private async Task OnRemoveFile()
        {
            SelectedFile = null;
            _src = "";
            _isFile = false;
            await SelectedFileChanged.InvokeAsync(null);
        }

        public async ValueTask DisposeAsync()
        {
            if (_dropZoneInstance != null)
            {
                await _dropZoneInstance.InvokeVoidAsync("dispose");
                await _dropZoneInstance.DisposeAsync();
            }

            if (_module != null)
            {
                await _module.DisposeAsync();
            }
        }
    }
}
