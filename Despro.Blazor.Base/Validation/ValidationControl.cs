using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq.Expressions;

namespace Despro.Blazor.Base.Validation;

public class ValidationControl<TValue> : ValidationMessage<TValue>
{
    [CascadingParameter] private EditContext EditContext { get; set; } = default!;
    [Parameter] public string CssClass { get; set; } = "invalid-feedback";
    [Parameter] public string FieldName { get; set; }

    private FieldIdentifier _fieldIdentifier;
    private Expression<Func<TValue>> _previousFieldAccessor;


    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        foreach (string message in EditContext.GetValidationMessages(_fieldIdentifier))
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", CssClass);
            builder.AddMultipleAttributes(2, AdditionalAttributes);
            builder.AddContent(3, FormatMessage(message));
            builder.CloseElement();
        }
    }

    private string FormatMessage(string message)
    {
        return FieldName is null ? message : message.Replace("[FieldName]", FieldName);
    }

    protected override void OnParametersSet()
    {
        if (EditContext is null)
            ArgumentNullException.ThrowIfNull(nameof(EditContext));

        if (For is null)
            ArgumentNullException.ThrowIfNull(nameof(For));

        if (For != _previousFieldAccessor)
        {
            if (For != null)
            {
                _fieldIdentifier = FieldIdentifier.Create(For);
                _previousFieldAccessor = For;
            }
        }
    }
}
