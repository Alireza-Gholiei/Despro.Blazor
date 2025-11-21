using Despro.Blazor.Base.Models;
using Despro.Blazor.Table.Components.Table;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Table.TableRepository.Interface.Table
{
    public interface ITable<TableItem>
    {
        bool ShowFooter { get; set; }
        bool ResetSortCycle { get; set; }
        bool ShowSearch { get; set; }
        bool MultiSelect { get; set; }
        int VisibleColumnCount { get; }
        List<TableItem> SelectedItems { get; set; }
        GridData<TableItem> Items { get; }
        IList<TableItem> CurrentItems { get; }
        IDataProvider<TableItem> DataProvider { get; set; }
        RenderFragment<TableItem> RowActionTemplate { get; set; }
        bool ShowCheckboxes { get; set; }
        Task FirstPage();
        Task SetPage(int pageNumber);
        Task NextPage();
        Task PreviousPage();
        Task LastPage();
        Task ClearSelectedItem();
        List<IColumn<TableItem>> Columns { get; }
        List<IColumn<TableItem>> VisibleColumns { get; }
        void AddColumn(IColumn<TableItem> column);
        void RemoveColumn(IColumn<TableItem> column);
        Task Search(IColumn<TableItem> column);
        Task ClearSearch(IColumn<TableItem> column);
        Task SelectAll();
        Task UnSelectAll();
        Task Update(bool resetPage = false, bool render = true);
        void SetPageSize(int pageSize);
        string GetColumnWidth();
        SelectAllStrategy SelectAllStrategy { get; set; }
    }

    public interface ITableState<out TableItem>
    {
        bool ShowFooter { get; set; }
        bool ShowSearch { get; set; }
        bool UseNaturalSort { get; set; }
        int VisibleColumnCount { get; }
        Task Update(bool resetPage = false, bool render = true);
        TableItem CurrentEditItem { get; }
    }

    public interface IPopupEditTable<TItem>
    {
        List<IColumn<TItem>> Columns { get; }
        List<IColumn<TItem>> VisibleColumns { get; }
        bool ShowCheckboxes { get; }
        TItem CurrentEditItem { get; }
        bool IsRowValid { get; }

    }

    public interface IInlineEditTable<TableItem>
    {
        List<IColumn<TableItem>> Columns { get; }
        List<IColumn<TableItem>> VisibleColumns { get; }
        bool ShowCheckboxes { get; }
        GridData<TableItem> Items { get; }
        TableItem CurrentEditItem { get; }
        bool IsRowValid { get; }
    }

    public interface ITableRow<TableItem>
    {
        List<IColumn<TableItem>> Columns { get; }
        List<IColumn<TableItem>> VisibleColumns { get; }
        GridData<TableItem> Items { get; }
        TableItem SelectedItem { get; }
        List<TableItem> SelectedItems { get; }
        bool ShowCheckboxes { get; }
        RenderFragment<TableItem> DetailsTemplate { get; }
        RenderFragment<TableItem> RowActionTemplate { get; set; }
        RenderFragment<TableItem> RowActionEndTemplate { get; set; }
        EventCallback<TableItem> OnItemSelected { get; }
        EventCallback<List<TableItem>> SelectedItemsChanged { get; }
        Task SetSelectedItem(TableItem item);
        Task RowClicked(TableItem item);
        bool KeyboardNavigation { get; }
    }

    public interface ITableRowActions<in TableItem>
    {
        string GetColumnWidth();
    }

    public interface IDetailsTable<TableItem>
    {
        int VisibleColumnCount { get; }
        Task ClearSelectedItem();
        RenderFragment<TableItem> DetailsTemplate { get; }
    }
}