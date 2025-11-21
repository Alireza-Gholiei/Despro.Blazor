using Despro.Blazor.Base.Components;
using Despro.Blazor.Table.TableGenerals.Table;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Table.Components.Table
{
    public class GroupHeaderTableRowBase<TableItem> : BaseComponent
    {
        [Parameter] public TableResult<object, TableItem> Group { get; set; }
        [Parameter] public EventCallback<TableResult<object, TableItem>> GroupChanged { get; set; }

        protected async Task ToogleExpanded()
        {
            Group.Expanded = !Group.Expanded;
            await GroupChanged.InvokeAsync(Group);
        }

        protected string ExpandedCss()
        {
            return Group.Expanded ? "arrow-down" : "arrow-right";
        }
    }
}