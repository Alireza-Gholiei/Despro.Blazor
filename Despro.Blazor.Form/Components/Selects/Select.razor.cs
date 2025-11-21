using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace Despro.Blazor.Form.Components.Selects;

public partial class Select<TItem, TValue> : BaseComponent
{
    [CascadingParameter] private EditContext? EditContext { get; set; }

    [Parameter] public List<TItem> Items { get; set; } = [];
    [Parameter] public TValue SelectedValue { get; set; }
    [Parameter] public EventCallback<TValue> SelectedValueChanged { get; set; }
    [Parameter] public EventCallback<TValue> Updated { get; set; }
    [Parameter] public Func<TItem, string> TextExpression { get; set; }
    [Parameter] public Func<TItem, TValue> ConvertExpression { get; set; }
    [Parameter] public Func<TItem, bool> DisabledExpression { get; set; }

    [Parameter] public bool ShowValidation { get; set; } = true;
    [Parameter] public Expression<Func<TValue>> For { get; set; }
    [Parameter] public string ErrorMessage { get; set; }

    [Parameter] public string Label { get; set; }
    [Parameter] public string ItemListEmptyText { get; set; } = "*بدون موارد*";
    [Parameter] public string NoSelectedText { get; set; } = "*انتخاب مورد*";
    [Parameter] public bool Clearable { get; set; } = true;

    private List<ListItem<TItem, TValue>> ItemList = new();
    private TValue _previousValue;
    private FieldIdentifier _fieldIdentifier;

    protected override void OnInitialized()
    {
        if (For != null && EditContext != null)
            _fieldIdentifier = FieldIdentifier.Create(For);

        if (ConvertExpression != null) return;

        if (typeof(TItem) != typeof(TValue))
        {
            throw new InvalidOperationException($"{GetType()} requires a {nameof(ConvertExpression)} parameter.");
        }

        ConvertExpression = item => item is TValue value ? value : default!;
    }

    protected override async Task OnParametersSetAsync()
    {
        PopulateItemList();

        if (!EqualityComparer<TValue>.Default.Equals(_previousValue, SelectedValue))
        {
            _previousValue = SelectedValue;

            if (!ItemNotInList())
            {
                await SelectedValueChanged.InvokeAsync(SelectedValue);
                await Updated.InvokeAsync(SelectedValue);
                NotifyFieldChanged();
            }
        }
    }

    private bool IsSelected(TValue value)
    {
        return EqualityComparer<TValue>.Default.Equals(SelectedValue, value);
    }

    private bool ItemNotInList()
    {
        if (SelectedValue == null) return true;
        return !ItemList.Any(i => EqualityComparer<TValue>.Default.Equals(i.Value, SelectedValue));
    }

    private void PopulateItemList()
    {
        ItemList = Items.Select(item =>
        {
            var listItem = new ListItem<TItem, TValue>
            {
                Text = GetText(item),
                Value = GetValue(item),
                Item = item,
                Disabled = DisabledExpression?.Invoke(item) ?? false
            };
            return listItem;
        }).ToList();
    }

    private async Task OnValueChanged(ChangeEventArgs e)
    {
        var id = e.Value?.ToString() ?? "";
        var listItem = ItemList.FirstOrDefault(v => v.Id == id);

        SelectedValue = listItem != null ? listItem.Value : default!;

        await SelectedValueChanged.InvokeAsync(SelectedValue);
        await Updated.InvokeAsync(SelectedValue);
        NotifyFieldChanged();
    }

    private TValue GetValue(TItem item)
    {
        return ConvertExpression != null ? ConvertExpression.Invoke(item) : default!;
    }

    private string GetText(TItem item)
    {
        return TextExpression?.Invoke(item) ?? item?.ToString() ?? string.Empty;
    }

    private async Task Clear()
    {
        SelectedValue = default!;
        await SelectedValueChanged.InvokeAsync(SelectedValue);
        await Updated.InvokeAsync(SelectedValue);
        NotifyFieldChanged();
    }

    private void NotifyFieldChanged()
    {
        if (EditContext != null && For != null)
            EditContext.NotifyFieldChanged(_fieldIdentifier);
    }

    private bool HasError => EditContext?.GetValidationMessages(_fieldIdentifier).Any() ?? false;

    protected override string ClassNames => ClassBuilder
        .Add("form-control form-select")
        .AddIf("is-invalid", HasError)
        .AddIf("is-valid", !HasError && SelectedValue != null)
        .ToString();
}