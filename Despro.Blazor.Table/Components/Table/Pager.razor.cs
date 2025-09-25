using Despro.Blazor.Base.Components;
using Despro.Blazor.Table.TableRepository.Interface.Table;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Table.Components.Table
{
    public class PagerBase<Item> : BaseComponent
    {
        [CascadingParameter(Name = "Table")]
        public ITable<Item> Table { get; set; }

        public bool ShowPageNumber { get; set; }
        protected int TotalPages { get; set; } //
        public int SkipQuantity { get; private set; }
        public int PageSize { get; set; } // Limit

        protected override void OnParametersSet()
        {
            TotalPages = Table.Items.PageCount;
            SkipQuantity = (Table.Items.CurrentPage - 1) * Table.Items.Limit;
            ShowPageNumber = Table.Items.EntityCount > Table.Items.Limit;
        }

        public string FirstItemNumber => (SkipQuantity + 1).ToString();
        public string LastItemNumber => Math.Min(SkipQuantity + Table.Items.Limit, Table.Items.EntityCount).ToString();

        public void SetPageSize(ChangeEventArgs obj)
        {
            Table.SetPageSize(int.Parse(obj.Value?.ToString()!));
        }
    }
}
