using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Despro.Blazor.Base.Models;
using Despro.Blazor.Base.Services;
using Despro.Blazor.Table.Components.Tables.Components;
using Despro.Blazor.Table.TableGenerals.Table;
using Despro.Blazor.Table.TableRepository.Interface.Table;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Table.Components.Table
{
    public class TableBase<TItem> : BaseComponent, IPopupEditTable<TItem>, ITable<TItem>, IInlineEditTable<TItem>, IDetailsTable<TItem>, ITableRow<TItem>, ITableState<TItem>
    {
        [Inject] private BaseService TabService { get; set; }

        [Parameter] public bool ShowHeader { get; set; } = true;
        [Parameter] public bool ShowTableHeader { get; set; } = true;
        [Parameter] public bool Selectable { get; set; } = false;
        [Parameter] public bool TableSearchable { get; set; } = true;
        [Parameter] public bool Hover { get; set; } = true;
        [Parameter] public bool Responsive { get; set; } = true;
        [Parameter] public bool ShowNoItemsLabel { get; set; } = true;
        [Parameter] public bool ShowCheckboxes { get; set; } = false;
        [Parameter] public bool ResetSortCycle { get; set; }
        [Parameter] public bool ShowFooter { get; set; } = true;
        [Parameter] public bool MultiSelect { get; set; }
        [Parameter] public bool KeyboardNavigation { get; set; }
        [Parameter] public bool UseNaturalSort { get; set; } = false;
        [Parameter] public string TableClass { get; set; } = "table card-table table-vcenter no-footer";
        [Parameter] public string ValidationRuleSet { get; set; } = "default";
        [Parameter] public RenderFragment<TItem> RowActionTemplate { get; set; }
        [Parameter] public RenderFragment<TItem> RowActionEndTemplate { get; set; }
        [Parameter] public RenderFragment<TItem> DetailsTemplate { get; set; }
        [Parameter] public RenderFragment HeaderTemplate { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<List<TItem>> SelectedItemsChanged { get; set; }
        [Parameter] public EventCallback<TItem> OnRowClicked { get; set; }
        [Parameter] public EventCallback<TItem> OnItemSelected { get; set; }
        [Parameter] public EventCallback<BaseGrid> OnBindGrid { get; set; }
        [Parameter] public OnCancelStrategy? CancelStrategy { get; set; } = OnCancelStrategy.AsIs;
        [Parameter] public SelectAllStrategy SelectAllStrategy { get; set; } = SelectAllStrategy.AllPages;
        [Parameter] public IDataProvider<TItem> DataProvider { get; set; }
        [Parameter] public GridData<TItem> Items { get; set; }
        [Parameter] public List<TItem> SelectedItems { get; set; }


        private bool TableInitialized { get; set; }
        public bool ReloadingItems { get; set; }
        public bool ChangedItem { get; set; } = true;
        public bool IsRowValid { get; set; }
        public bool ShowSearch { get; set; } = false;
        public int VisibleColumnCount => Columns.Count(x => x.Visible) + (ShowCheckboxes ? 1 : 0);
        public TItem SelectedItem { get; set; }
        private TItem StateBeforeEdit { get; set; }
        public TItem CurrentEditItem { get; private set; }
        public IList<TItem> CurrentItems => Items.Data ?? TempItems?.FirstOrDefault();
        protected IEnumerable<TableResult<object, TItem>> TempItems { get; set; } = new List<TableResult<object, TItem>>();
        protected IDictionary<string, object> Attributes { get; set; }
        public List<IColumn<TItem>> Columns { get; } = new();
        public List<IColumn<TItem>> SearchColumns { get; } = new();
        public List<IColumn<TItem>> VisibleColumns => Columns.Where(x => x.Visible).ToList();
        protected ElementReference Table { get; set; }

        public async Task Search(IColumn<TItem> column)
        {
            try
            {
                SearchColumns.Add(column);

                await Update(true);
            }
            catch (Exception)
            {
                await Update();
            }
        }

        public async Task ClearSearch(IColumn<TItem> column)
        {
            try
            {
                column.SearchText = string.Empty;

                _ = SearchColumns.Remove(column);

                await Update(true);
            }
            catch (Exception)
            {
                await Update();
            }
        }

        public async Task UnSelectAll()
        {
            SelectedItems.Clear();
            SelectedItem = default;
            await UpdateSelected();
        }

        public async Task SelectAll()
        {
            if (CurrentItems == null || !CurrentItems.Any()) return;
            if (SelectAllStrategy == SelectAllStrategy.AllPages)
            {
                int currentPageNumber = Items.CurrentPage;
                int currentPageSize = Items.Limit;
                Items.CurrentPage = 0;
                Items.Limit = int.MaxValue;
                SelectedItems = DataProvider.GetData(Columns, Columns.FirstOrDefault(x => x.SortColumn), this, null).Result.First();
                Items.CurrentPage = currentPageNumber;
                Items.Limit = currentPageSize;
            }
            else
            {
                SelectedItems = CurrentItems.ToList();
            }

            SelectedItem = SelectedItems.First();
            await UpdateSelected();
        }

        public Task ClearSelectedItem()
        {
            //if (ChangedItem)
            //{
            //    ChangedItem = false;
            //    return;
            //}

            //SelectedItem = default;
            //await Update();
            return Task.CompletedTask;
        }

        public async Task Update(bool resetPage = false, bool render = true)
        {
            if (CurrentEditItem == null || !TempItems.Any())
            {
                TempItems = await DataProvider.GetData(SearchColumns, Columns.FirstOrDefault(x => x.SortColumn), this, Items, resetPage);

                if (render)
                {
                    ReloadingItems = true;

                    BaseGrid baseGrid = new()
                    {
                        CurrentPage = Items.CurrentPage,
                        FilterParam = Items.FilterParam,
                        Limit = Items.Limit,
                        OrderField = Items.OrderField,
                        OrderType = Items.OrderType
                    };

                    await OnBindGrid.InvokeAsync(baseGrid);

                    TempItems = await DataProvider.GetData(SearchColumns, Columns.FirstOrDefault(x => x.SortColumn), this, Items, resetPage);

                    ReloadingItems = false;
                }

                await Refresh();
            }
        }

        public void AddColumn(IColumn<TItem> column)
        {
            Columns.Add(column);
            StateHasChanged();
        }

        public void RemoveColumn(IColumn<TItem> column)
        {
            _ = Columns.Remove(column);
            StateHasChanged();
        }

        public async Task SetPage(int pageNumber)
        {
            Items.CurrentPage = pageNumber;
            await Update();
        }

        public async Task FirstPage()
        {
            if (Items.CurrentPage != 1)
            {
                Items.CurrentPage = 1;
                await Update();
            }
        }

        public async Task NextPage()
        {
            if (Items.CurrentPage < Items.EntityCount / Items.Limit)
            {
                Items.CurrentPage++;
                await Update();
            }
        }

        public async Task PreviousPage()
        {
            if (Items.CurrentPage >= 1)
            {
                Items.CurrentPage--;
                await Update();
            }
        }

        public async Task LastPage()
        {
            Items.CurrentPage = (int)Math.Ceiling((decimal)Items.EntityCount / Items.Limit);
            await Update();
        }

        public void SetPageSize(int pageSize)
        {
            Items.Limit = pageSize;
            _ = Update();
        }

        public string GetColumnWidth()
        {
            int width = 16;
            return Math.Max(width, 80) + "px";
        }

        public async Task SetSelectedItem(TItem item)
        {
            SelectedItems ??= new List<TItem>();

            if (IsSelected(item))
            {
                _ = SelectedItems.Remove(item);
                SelectedItem = default;
            }
            else
            {
                if (!MultiSelect)
                {
                    SelectedItems.Clear();
                }

                SelectedItems.Add(item);
            }

            SelectedItem = item;
            await UpdateSelected();
        }

        public async Task RowClicked(TItem item)
        {
            if (Selectable)
            {
                if (!ShowCheckboxes)
                {
                    await SetSelectedItem(item);
                }

                await OnRowClicked.InvokeAsync(item);
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            await Update(render: false);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender && !TableInitialized)
            {
                if (KeyboardNavigation)
                {
                    await TabService.PreventDefaultKey(Table, "keydown", ["ArrowUp", "ArrowDown"]);
                }

                TableInitialized = true;
                await Update();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            DataProvider ??= new TheGridDataFactory<TItem>();
            if (Hover)
            {
                TableClass += " table-hover";
            }

            Dictionary<string, object> baseAttributes = new()
            {
                {"class", TableClass}
            };

            if (UnmatchedParameters?.TryGetValue("class", out object parameter) is true)
            {
                baseAttributes["class"] = TableClass + " " + parameter;
                //var dictionary = baseAttributes.Union(UnknownParameters.Where(x => x.Key != "class")).ToDictionary(x => x.Key, x => x.Value);
            }

            Attributes = baseAttributes;

            await Update(render: false);
        }

        public string GetTableCssClass()
        {
            ClassBuilder classBuileder = new();
            return classBuileder
                .Add("tabler-table")
                .AddIf("table-responsive", Responsive)
                .ToString();
        }

        public bool IsSelected(TItem item)
        {
            return SelectedItems == null ? false : SelectedItems.Contains(item);
        }

        protected bool ShowDetailsRow(TItem item)
        {
            return DetailsTemplate != null && IsSelected(item);
        }

        private async Task UpdateSelected()
        {
            await OnItemSelected.InvokeAsync(SelectedItem);
            await SelectedItemsChanged.InvokeAsync(SelectedItems);
            await Update();
        }

        public async Task Refresh()
        {
            await InvokeAsync(StateHasChanged);
        }

    }

    public enum SelectAllStrategy
    {
        AllPages = 0,
        CurrentPage = 1
    }
}