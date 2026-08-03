using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Layout.Components.Icons;
using Despro.Blazor.Layout.Components.IconsModel;
using Despro.Blazor.Table.TableGenerals.Table;
using Despro.Blazor.Table.TableRepository.Interface.Table;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Table.Components.Table;

public class TableHeaderBase<TableItem> : TableRowComponentBase<TableItem>
{
    [CascadingParameter(Name = "Table")] public ITable<TableItem> Table { get; set; }

    public string GetColumnHeaderClass(IColumn<TableItem> column)
    {
        return new ClassBuilder()
            .AddIf("cursor-pointer", column.Sortable)
            .AddIf("text-end", column.Align == Align.End)
            .ToString();
    }

    protected string GetSortIconClass(IColumn<TableItem> column)
    {
        return column is { SortColumn: false, Sortable: true }
            ? "sorting"
            : column is { SortColumn: true, SortDescending: true }
                ? "sorting_desc"
                : column is { SortColumn: true, SortDescending: false } ? "sorting_desc" : string.Empty;
    }

    protected IIconType GetSortIcon(IColumn<TableItem> column)
    {
        return column is { SortColumn: false, Sortable: true }
            ? InternalIcons.Sortable
            : column is { SortColumn: true, SortDescending: true }
                ? InternalIcons.Sort_Desc
                : column is { SortColumn: true, SortDescending: false } ? InternalIcons.Sort_Asc : null;
    }

    protected bool? SelectedValue()
    {
        return Table.SelectedItems == null || !Table.SelectedItems.Any()
            ? false
            : Table.SelectAllStrategy == SelectAllStrategy.AllPages && Table.SelectedItems.Count == Table.Items.EntityCount
                ? true
                : Table.SelectAllStrategy != SelectAllStrategy.AllPages && Table.SelectedItems.Count == Table.CurrentItems.Count ? true : null;
    }

    protected void ToggleSelected(bool? value)
    {
        var selected = SelectedValue();
        _ = selected != true ? Table.SelectAll() : Table.UnSelectAll();
    }
}