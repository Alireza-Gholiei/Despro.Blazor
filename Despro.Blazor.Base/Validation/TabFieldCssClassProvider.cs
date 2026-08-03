using Microsoft.AspNetCore.Components.Forms;

namespace Despro.Blazor.Base.Validation;

public class TabFieldCssClassProvider : FieldCssClassProvider
{
    public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
    {
        var isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();
        var isModified = editContext.IsModified(fieldIdentifier);
        return isModified ? isValid ? "is-valid" : "is-invalid" : !isValid ? "is-invalid" : "";
    }
}