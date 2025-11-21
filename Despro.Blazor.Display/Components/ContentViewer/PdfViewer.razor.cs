using Despro.Blazor.Base.Components;
using Despro.Blazor.Base.Extentions;
using Despro.Blazor.Base.Services;
using Despro.Blazor.Display.Models;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Display.Components.ContentViewer
{
    public partial class PdfViewer : BaseComponent, IAsyncDisposable
    {
        [Inject] BaseService _baseService { get; set; }
        [Parameter] public byte[] Content { get; set; }
        [Parameter] public DocumentContentTypes ContentType { get; set; }
        [Parameter] public string UrlSuffix { get; set; }

        private string objectURL;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (Content != null && objectURL == null)
            {
                var _contentType = ContentType.GetDescription();

                objectURL = await _baseService.CreateObjectURLAsync(_contentType, Content);

                StateHasChanged();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (objectURL != null)
            {
                await _baseService.RevokeObjectURLAsync(objectURL);
            }
        }
    }
}