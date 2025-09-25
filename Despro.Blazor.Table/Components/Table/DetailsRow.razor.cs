using Despro.Blazor.Base.Components;
using Despro.Blazor.Table.TableRepository.Interface.Table;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Table.Components.Table
{
    public class DetailsRowBase<TableItem> : BaseComponent
    {
        [Parameter] public IDetailsTable<TableItem> Table { get; set; }
        [Parameter] public TableItem Item { get; set; }
    }
}