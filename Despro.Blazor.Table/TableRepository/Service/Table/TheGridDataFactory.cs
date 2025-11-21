using Despro.Blazor.Base.Models;
using Despro.Blazor.Table.TableGenerals.Table;
using Despro.Blazor.Table.TableRepository.Interface.Table;
using Despro.Blazor.Table.TableRepository.Service.Table;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Despro.Blazor.Table.Components.Tables.Components
{
    public partial class TheGridDataFactory<Item> : IDataProvider<Item>
    {
        public async Task<IEnumerable<TableResult<object, Item>>> GetData(List<IColumn<Item>> searchColumns,
            IColumn<Item> sortColumn, ITableState<Item> state, GridData<Item> items,
            bool resetPage = false, bool addSorting = true,
            Item moveToItem = default)
        {
            try
            {
                List<TableResult<object, Item>> viewResult = new();
                if (items is not null && items.Data is not null)
                {
                    items.FilterParam.Clear();
                    foreach (var column in searchColumns.Where(x => x.Searchable))
                    {
                        AddSearch(column, state, items);
                    }

                    if (addSorting)
                    {
                        AddSorting(sortColumn, state, items);
                    }

                    if (resetPage)
                    {
                        items.CurrentPage = 1;
                    }

                    viewResult.Add(new TableResult<object, Item>(null, items.Data));
                }
                return await Task.FromResult(viewResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void AddSearch(IColumn<Item> column, ITableState<Item> state, GridData<Item> query)
        {
            if (!string.IsNullOrEmpty(column.SearchText))
            {
                var filter = new FilterParam(column.SearchExpression ?? column.Property.GetPropertyMemberInfo().Name, column.SearchText);

                var exist = query.FilterParam.FirstOrDefault(x => x.Key == filter.Key);

                if (exist != null)
                {
                    var index = query.FilterParam.IndexOf(exist);
                    query.FilterParam[index].Key = exist.Key;
                    query.FilterParam[index].Operator = exist.Operator;
                    query.FilterParam[index].SearchExpression = exist.SearchExpression;
                    query.FilterParam[index].Value = exist.Value;
                }
                else
                {
                    query.FilterParam.Add(filter);
                }
            }
        }

        private void AddSorting(IColumn<Item> sortColumn, ITableState<Item> state, GridData<Item> query)
        {
            if (sortColumn != null)
            {
                if (state.UseNaturalSort)
                {
                    NaturalOrderBy(query, sortColumn.Property, sortColumn.SortDescending);
                }
                else
                {
                    query.OrderField = sortColumn.Property.GetPropertyMemberInfo().Name;
                    query.OrderType = sortColumn.SortDescending ? OrderType.Descending : OrderType.Ascending;
                }
            }
        }

        [GeneratedRegex("\\d+")]
        private static partial Regex DigitRegex();

        private static void NaturalOrderBy<T>(GridData<T> source, Expression<Func<T, object>> selectorExpr, bool desc)
        {
            //var selector = selectorExpr.Compile();
            //var max = source
            //    .SelectMany(i => DigitRegex().Matches(selector(i).ToString()).Select(m => (int?)m.Value.Length))
            //    .Max() ?? 0;
            //Expression<Func<T, string>> keySelector = i => DigitRegex().Replace(selector(i).ToString(), m => m.Value.PadLeft(max, '0'));
            //return desc ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
        }
    }
}