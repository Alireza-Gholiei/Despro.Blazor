using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Despro.Blazor.Form.Components.FormControls
{
    public partial class FormInputNumber<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TValue> : BaseComponent
    {
        [Parameter] public TValue Value { get; set; }
        [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
        [Parameter] public string Label { get; set; }
        [Parameter] public bool LabelDisplay { get; set; } = true;
        [Parameter] public bool Validate { get; set; } = true;
        public bool IsValid { get; set; } = false;

        private TValue currentValue;
        private TValue CurrentValue
        {
            get => currentValue;

            set
            {
                currentValue = value;

                IsValid = BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out _);

                if (IsValid)
                {
                    ValueChanged.InvokeAsync(value);
                }
            }
        }
    }
}
