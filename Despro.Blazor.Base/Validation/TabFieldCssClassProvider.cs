using Microsoft.AspNetCore.Components.Forms;

namespace Despro.Blazor.Base.Validation;

public class TabFieldCssClassProvider : FieldCssClassProvider
{
    public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
    {
        bool isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();
        bool isModified = editContext.IsModified(fieldIdentifier);
        return isModified ? isValid ? "is-valid" : "is-invalid" : !isValid ? "is-invalid" : "";
    }
}