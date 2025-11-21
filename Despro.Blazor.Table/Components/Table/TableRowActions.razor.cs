using Despro.Blazor.Table.TableRepository.Interface.Table;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Table.Components.Table
{
    public class TableRowActionsBase<TableItem> : TableRowComponentBase<TableItem>
    {
        [Parameter] public ITableRowActions<TableItem> Table { get; set; }
        [Parameter] public TableItem Item { get; set; }
    }
}