using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq.Expressions;

namespace Despro.Blazor.Base.Validation;

public class ValidationControl<TValue> : ComponentBase, IDisposable
{
    [CascadingParameter] private EditContext EditContext { get; set; } = null!;
    [Parameter] public Expression<Func<TValue>> For { get; set; }
    [Parameter] public string CssClass { get; set; } = "invalid-feedback";
    [Parameter] public string FieldName { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> AdditionalAttributes { get; set; }

    private FieldIdentifier _fieldIdentifier;
    private IReadOnlyCollection<string> _messages = [];

    protected override void OnParametersSet()
    {
        if (EditContext == null)
            throw new ArgumentNullException(nameof(EditContext));

        if (For == null)
            throw new ArgumentNullException(nameof(For));

        _fieldIdentifier = FieldIdentifier.Create(For);

        EditContext.OnValidationStateChanged -= EditContext_OnValidationStateChanged;
        EditContext.OnValidationStateChanged += EditContext_OnValidationStateChanged;

        _messages = EditContext.GetValidationMessages(_fieldIdentifier).ToArray();
    }

    private void EditContext_OnValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
    {
        var currentMessages = EditContext.GetValidationMessages(_fieldIdentifier).ToArray();
        if (currentMessages.SequenceEqual(_messages)) return;

        _messages = currentMessages;
        InvokeAsync(StateHasChanged);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var seq = 0;
        foreach (var message in _messages)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", CssClass);
            builder.AddMultipleAttributes(seq++, AdditionalAttributes);
            builder.AddContent(seq++, FormatMessage(message));
            builder.CloseElement();
        }
    }

    private string FormatMessage(string message)
    {
        return string.IsNullOrEmpty(FieldName) ? message : message.Replace("[FieldName]", FieldName);
    }

    public void Dispose()
    {
        if (EditContext != null)
            EditContext.OnValidationStateChanged -= EditContext_OnValidationStateChanged;
    }
}
