using Despro.Blazor.Base.Components;
using Despro.Blazor.Base.Validation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Despro.Blazor.Form.Components
{
    public partial class Form : BaseComponent
    {
        [Inject] protected IServiceProvider Provider { get; set; }

        [Parameter] public EventCallback<EditContext> OnValidSubmit { get; set; }
        [Parameter] public EventCallback<bool> IsValidChanged { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public object Model { get; set; }
        [Parameter] public IFormValidator Validator { get; set; }
        [Parameter] public bool IsValid { get; set; }
        [Parameter] public bool IsSubmitBtn { get; set; } = true;
        [Parameter] public string SubmitText { get; set; } = "ثبت و ارسال";
        [Parameter] public string FormName { get; set; } = "";

        protected EditContext EditContext { get; set; }
        public DynamicComponent ValidatorInstance { get; set; }

        public bool IsModified => true;
        public bool RenderForm { get; set; }
        public bool CanSubmit => IsValid && IsModified;
        public bool NotValid { get; set; }
        private bool Submited { get; set; }
        private bool Initialized { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (string.IsNullOrEmpty(FormName))
            {
                FormName = Model.GetType().ToString() + "Form";
            }

            await SetupFormAsync();
        }

        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(DataAnnotationsValidator))]
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private async Task SetupFormAsync()
        {
            if (Model == null)
            {
                RenderForm = false;
                EditContext = null;
                return;
            }

            Validator = GetValidator();

            if (EditContext == null || !EditContext.Model.Equals(Model))
            {
                EditContext = new EditContext(Model);
                _ = await ValidateAsync();
            }

            EditContext.SetFieldCssClassProvider(new TabFieldCssClassProvider());

            RenderForm = true;
        }

        private IFormValidator GetValidator() => Validator ?? Provider.GetRequiredService<IFormValidator>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (RenderForm)
            {
                bool valid = await ValidateAsync();
                OnAfterModelValidation(valid);
            }
        }

        public void OnAfterModelValidation(bool isValid)
        {
            if (isValid != IsValid || !Initialized)
            {
                Initialized = true;
                IsValid = isValid;
                StateHasChanged();
                _ = IsValidChanged.InvokeAsync(IsValid);
            }
        }

        public async Task<bool> ValidateAsync()
        {
            bool valid = await Validator.ValidateAsync(ValidatorInstance?.Instance, EditContext);
            OnAfterModelValidation(valid);

            return IsValid;
        }

        public bool Validate()
        {
            bool valid = Validator.Validate(ValidatorInstance.Instance, EditContext);
            OnAfterModelValidation(valid);

            return IsValid;
        }

        protected async Task HandleValidSubmit()
        {
            if (CanSubmit)
            {
                NotValid = false;
                Submited = true;
                await OnValidSubmit.InvokeAsync(EditContext);
                EditContext?.MarkAsUnmodified();
                Submited = false;
            }
            else
            {
                NotValid = true;
            }
        }

        public void Dispose()
        {
            EditContext = null;
        }

        protected string GetSaveButtonClass()
        {
            return "";
        }
    }
}