using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Despro.Blazor.Modal.ModalGenerals;
using Despro.Blazor.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Modal.Components.Offcanvas;

public partial class OffcanvasContainer : BaseComponent
{
    [Inject] private IOffcanvasService offcanvasService { get; set; }

    protected override void OnInitialized()
    {
        offcanvasService.OnChanged += StateHasChanged99;

        base.OnInitialized();
    }

    private void OnClickOutside(OffcanvasModel model)
    {
        if (model.Options.CloseOnClickOutside)
        {
            offcanvasService.Close();
        }
    }

    protected void OnKeyDown(KeyboardEventArgs e, OffcanvasModel offcanvasModel)
    {
        if (e.Key == "Escape" && offcanvasModel.Options.CloseOnEsc)
        {
            offcanvasService.Close();
        }
    }

    private void StateHasChanged99()
    {
        StateHasChanged();
    }

    private string GetClasses(OffcanvasModel offcanvasModel) => new ClassBuilder()
        .Add("offcanvas")
        .Add($"offcanvas-{offcanvasModel.Options.Position.ToString().ToLower()}")
        .Add(offcanvasModel.Options.WrapperCssClass)
        .Add("show")
        .ToString();
}