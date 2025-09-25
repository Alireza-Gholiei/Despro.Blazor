using Despro.Blazor.Base.Components;
using Despro.Blazor.Base.Extentions;
using Despro.Blazor.Display.Models;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Display.Components.ContentViewer
{
    public partial class ImageViewer : BaseComponent
    {
        [Parameter] public byte[] Content { get; set; }
        [Parameter] public ImageContentTypes ContentType { get; set; }

        private string Src { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (Content != null)
            {
                var _contentType = ContentType.GetDescription();

                Src = $"data:{_contentType};base64,{Convert.ToBase64String(Content)}";

                StateHasChanged();
            }
        }
    }
}
