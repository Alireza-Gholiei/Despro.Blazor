using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Despro.Blazor.Form.Components.ValueInputs;

public partial class ValueInput<TValue> : BaseComponent
{
    private string currentValue;

    [Parameter] public TValue Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public string Label { get; set; }

    public bool IsValid { get; set; } = false;

    private string CurrentValue
    {
        get => currentValue;

        set
        {
            currentValue = value;

            IsValid = BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out _);

            //if (IsValid)
            //{
            //    ValueChanged.InvokeAsync((TValue)value);
            //}
        }
    }

    protected override void OnParametersSet()
    {
        if (Value != null)
        {
            currentValue = Value.ToString();
        }
        base.OnParametersSet();
    }

    protected override string ClassNames => ClassBuilder
        .Add("form-control")
        .AddIf("is-invalid", !IsValid)
        .ToString();
}
