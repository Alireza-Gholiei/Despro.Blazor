using Despro.Blazor.Base.Components;
using Despro.Blazor.Modal.ModalGenerals;
using Despro.Blazor.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Modal.Components.Modal.Standard
{
    public partial class DialogModal : BaseComponent
    {
        [Inject] private IModalService ModalService { get; set; }

        [Parameter] public DialogOptions Options { get; set; }

        private string[] textArray { get; set; } = [];

        protected override void OnInitialized()
        {
            string[] newLineArray = { Environment.NewLine };
            textArray = Options.SubText.Split(newLineArray, StringSplitOptions.None);
            _ = Options.SubText.Split(Environment.NewLine.ToArray(), StringSplitOptions.None);
        }

        private void Cancel()
        {
            ModalService.Close();
        }

        private void Ok()
        {
            ModalService.Close(ModalResult.Ok());
        }
    }
}
