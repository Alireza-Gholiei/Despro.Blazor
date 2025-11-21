using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq.Expressions;

namespace Despro.Blazor.Base.Validation;

public class ValidationControlOverride<TValue> : ComponentBase, IDisposable
{
    [CascadingParameter] private EditContext EditContext { get; set; } = null!;
    [Parameter] public Expression<Func<TValue>> For { get; set; }
    [Parameter] public string FieldName { get; set; }
    [Parameter] public string CssClass { get; set; } = "invalid-feedback";
    [Parameter] public string ErrorMessage { get; set; }
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

        UpdateMessages();
    }

    private void EditContext_OnValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
    {
        UpdateMessages();
    }

    private void UpdateMessages()
    {
        var mags = EditContext.GetValidationMessages(_fieldIdentifier)
                              .Select(m => IsDefaultRequiredMessage(m)
                                  ? GetOverrideMessage()
                                  : m)
                              .ToArray();

        if (mags.SequenceEqual(_messages)) return;

        _messages = mags;
        InvokeAsync(StateHasChanged);
    }

    private bool IsDefaultRequiredMessage(string message)
    {
        var result = !string.IsNullOrEmpty(FieldName) || message.Contains("field is required");

        return result;
    }

    private string GetOverrideMessage()
    {
        if (!string.IsNullOrEmpty(ErrorMessage)) return ErrorMessage;
        return !string.IsNullOrEmpty(FieldName) ? $"لطفاً {FieldName} را وارد کنید" : "این فیلد اجباری است";
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var seq = 0;
        foreach (var message in _messages)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", CssClass);
            builder.AddMultipleAttributes(seq++, AdditionalAttributes);
            builder.AddContent(seq++, message);
            builder.CloseElement();
        }
    }

    public void Dispose()
    {
        if (EditContext != null)
            EditContext.OnValidationStateChanged -= EditContext_OnValidationStateChanged;
    }
}
