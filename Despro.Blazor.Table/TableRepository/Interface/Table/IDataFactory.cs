using Despro.Blazor.Base.Models;
using Despro.Blazor.Table.TableGenerals.Table;

namespace Despro.Blazor.Table.TableRepository.Interface.Table
{
    public interface IDataProvider<Item>
    {
        public Task<IEnumerable<TableResult<object, Item>>> GetData(List<IColumn<Item>> searchColumns, IColumn<Item> sortColumn,
            ITableState<Item> state, GridData<Item> items, bool resetPage = false, bool addSorting = true,
            Item moveToItem = default);
    }
}
