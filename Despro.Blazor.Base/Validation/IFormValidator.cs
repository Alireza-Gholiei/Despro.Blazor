using Microsoft.AspNetCore.Components.Forms;

namespace Despro.Blazor.Base.Validation;

public interface IFormValidator
{
    Type Component { get; }
    Task<bool> ValidateAsync(object validatorInstance, EditContext editContext);
    bool Validate(object validatorInstance, EditContext editContext);
}