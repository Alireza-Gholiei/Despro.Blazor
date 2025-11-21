using Microsoft.AspNetCore.Components.Forms;

namespace Despro.Blazor.Base.Validation;

public class BaseDataAnnotationsValidator : IFormValidator
{
    public Type Component => typeof(DataAnnotationsValidator);

    public Task<bool> ValidateAsync(object validatorInstance, EditContext editContext)
    {
        return Task.FromResult(editContext.Validate());
    }

    public bool Validate(object validatorInstance, EditContext editContext)
    {
        return editContext.Validate();
    }
}