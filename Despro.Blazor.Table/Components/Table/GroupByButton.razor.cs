using Despro.Blazor.Base.Components;
using Despro.Blazor.Table.TableRepository.Interface.Table;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Table.Components.Table
{
    public partial class GroupByButtonBase<Item> : BaseComponent
    {
        [CascadingParameter(Name = "Table")] public ITable<Item> Table { get; set; }

        protected async Task SetGroup(IColumn<Item> column)
        {
            await column.GroupByMeAsync();
        }
    }
}

