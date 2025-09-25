using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Form.Components.Selects
{
    public partial class Select<TItem, TValue> : BaseComponent
    {
        [Parameter] public List<TItem> Items { get; set; }
        [Parameter] public TValue SelectedValue { get; set; }
        [Parameter] public EventCallback<TValue> SelectedValueChanged { get; set; }
        [Parameter] public EventCallback Updated { get; set; }
        [Parameter] public Func<TItem, string> TextExpression { get; set; }
        [Parameter] public Func<TItem, TValue> ConvertExpression { get; set; }
        [Parameter] public Func<TItem, bool> DisabledExpression { get; set; }
        [Parameter] public string ItemListEmptyText { get; set; } = "*بدون موارد*";
        [Parameter] public string NoSelectedText { get; set; } = "*انتخاب*";
        [Parameter] public bool Clearable { get; set; } = true;


        protected List<ListItem<TItem, TValue>> ItemList = new();
        private bool IsValid = false;

        protected override void OnInitialized()
        {
            if (ConvertExpression == null)
            {
                if (typeof(TItem) != typeof(TValue))
                {
                    throw new InvalidOperationException($"{GetType()} requires a {nameof(ConvertExpression)} parameter.");
                }

                ConvertExpression = item => item is TValue value ? value : default;
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            PopulateItemList();
        }


        protected override string ClassNames => ClassBuilder
          .Add("form-control form-select")
          .AddIf("is-invalid", !IsValid)
          .AddIf("is-valid", IsValid)
          .ToString();

        protected bool IsSelected(TValue value)
        {
            return EqualityComparer<TValue>.Default.Equals(SelectedValue, value);
        }

        protected bool ItemNotInList()
        {
            if (SelectedValue == null) return true;
            foreach (ListItem<TItem, TValue> item in ItemList)
            {
                if (IsSelected(item.Value)) return false;
            }
            return true;
        }

        private void PopulateItemList()
        {
            if (Items != null)
            {
                ItemList = new List<ListItem<TItem, TValue>>();

                foreach (TItem item in Items)
                {
                    ListItem<TItem, TValue> listItem = new()
                    {
                        Text = GetText(item),
                        Value = GetValue(item),
                        Item = item
                    };

                    if (DisabledExpression != null)
                    {
                        listItem.Disabled = DisabledExpression(item);
                    }

                    ItemList.Add(listItem);
                }
            }
        }

        protected async void ValueChanged(ChangeEventArgs e)
        {
            string id = e.Value?.ToString();
            ListItem<TItem, TValue> listItem = ItemList.FirstOrDefault(v => v.Id == id);

            SelectedValue = listItem != null ? listItem.Value : default;

            IsValid = listItem != null ? true : false;

            await SelectedValueChanged.InvokeAsync(SelectedValue);
            await Updated.InvokeAsync(null);
        }

        protected TValue GetValue(TItem item)
        {
            return ConvertExpression == null ? default : ConvertExpression.Invoke(item);
        }

        private string GetText(TItem item)
        {
            return TextExpression == null ? item.ToString() : TextExpression.Invoke(item);
        }

        protected async void Clear()
        {
            SelectedValue = default;
            await SelectedValueChanged.InvokeAsync(SelectedValue);
        }
    }
}
