using Despro.Blazor.Base.Components;
using Despro.Blazor.Table.TableRepository.Interface.Table;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Table.Components.Table
{
    public class AllColumnRowBase<TableItem> : BaseComponent
    {
        [CascadingParameter(Name = "Table")] public ITable<TableItem> Table { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string CssClass { get; set; }
    }
}