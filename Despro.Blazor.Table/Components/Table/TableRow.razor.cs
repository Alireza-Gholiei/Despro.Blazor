using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Services;
using Despro.Blazor.Table.TableRepository.Interface.Table;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Table.Components.Table
{
    public class TableRowBase<TTableItem> : TableRowComponentBase<TTableItem>
    {
        [Inject] private BaseService TabService { get; set; }

        [Parameter] public ITableRow<TTableItem> Table { get; set; }
        [Parameter] public TTableItem Item { get; set; }
        [Parameter] public ITableRowActions<TTableItem> Actions { get; set; }
        [Parameter] public int Index { get; set; }
        [Parameter] public int PageNumber { get; set; }
        [Parameter] public int PageSize { get; set; }
        [Parameter] public bool IsSearch { get; set; } = false;


        protected ElementReference[] TableCells;

        protected override void OnInitialized()
        {
            TableCells = new ElementReference[Table.VisibleColumns.Count + 2];
        }

        protected int GetTabIndex()
        {
            return Table.KeyboardNavigation ? 0 : -1;
        }

        public string GetRowCssClass(TTableItem item)
        {
            return new ClassBuilder()
               .Add("data-row")
               .AddIf("table-active", IsSelected(item) && (Table.OnItemSelected.HasDelegate || Table.SelectedItemsChanged.HasDelegate))
               .ToString();
        }

        protected async Task OnKeyDown(KeyboardEventArgs e, ElementReference tableCell)
        {
            if (e.Key == "ArrowUp" || e.Key == "ArrowDown")
            {
                await TabService.NavigateTable(tableCell, e.Key);
            }
        }

        public async Task RowClick()
        {
            await Table.RowClicked(Item);
        }

        public bool IsSelected(TTableItem item)
        {
            return Table.SelectedItems == null ? false : Table.SelectedItems.Contains(item);
        }
    }
}