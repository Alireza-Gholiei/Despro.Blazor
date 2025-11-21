using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Linq.Expressions;

namespace Despro.Blazor.Display.Components.TreeViews
{
    public partial class TreeView<TItem> : BaseComponent
    {
        [Parameter] public RenderFragment<TItem> Template { get; set; }
        [Parameter] public Func<TItem, Task<IList<TItem>>> ChildSelectorAsync { get; set; }
        [Parameter] public Func<TItem, IList<TItem>>? ChildSelector { get; set; }
        [Parameter] public Func<TItem, bool> DefaultExpanded { get; set; }
        [Parameter] public EventCallback<List<TItem>> ExpandedItemsChanged { get; set; }
        [Parameter] public EventCallback<ItemDropped<TItem>> ItemDropped { get; set; }
        [Parameter] public EventCallback<TItem> ItemDragged { get; set; }
        [Parameter] public List<TItem> SelectedItems { get; set; }
        [Parameter] public EventCallback<TItem> SelectedItemChanged { get; set; }
        [Parameter] public Expression<Func<TItem>> SelectedItemExpression { get; set; }
        [Parameter] public List<TItem> CheckedItems { get; set; }
        [Parameter] public EventCallback<List<TItem>> CheckedItemsChanged { get; set; }
        [Parameter] public TItem SelectedItem { get; set; }
        [Parameter] public EventCallback<List<TItem>> SelectedItemsChanged { get; set; }
        [Parameter] public IList<TItem> Items { get; set; }
        [Parameter] public CheckboxMode CheckboxMode { get; set; }
        [Parameter] public bool EnableDragAndDrop { get; set; }
        [Parameter] public bool AlwaysExpanded { get; set; }
        [Parameter] public bool MultiSelect { get; set; }
        [Parameter] public bool AlignTreeNodes { get; set; }


        private readonly List<TItem> expandedItems = [];
        private List<TItem> selectedItems = [];
        private List<TItem> checkedItems = [];

        public TItem DraggedItem;

        protected override async Task OnInitializedAsync()
        {
            SetChildSelector();
            if (DefaultExpanded != null)
            {
                await SetDefaultExpandedAsync(Items);
            }
        }

        protected override void OnParametersSet()
        {
            SetChildSelector();

            checkedItems = CheckedItems ?? [];

            if (MultiSelect)
            {
                selectedItems = SelectedItems;
            }
            else
            {
                selectedItems.Clear();
                if (SelectedItem != null)
                {
                    selectedItems.Add(SelectedItem);
                }
            }
        }

        internal async Task SetDraggedAsync(TItem item)
        {
            DraggedItem = item;
            await ItemDragged.InvokeAsync(item);
        }

        internal async Task SetDroppedAsync(TItem targetItem, DragEventArgs e)
        {
            if (DraggedItem != null && targetItem != null && !targetItem.Equals(DraggedItem))
            {
                await ItemDropped.InvokeAsync(new ItemDropped<TItem> { Item = DraggedItem, TargetItem = targetItem, DragEventArgs = e });
            }
            DraggedItem = default;
        }

        private void SetChildSelector()
        {
            ChildSelectorAsync = ChildSelectorAsync switch
            {
                null when ChildSelector == null => e => null,
                null when ChildSelector != null => e => Task.FromResult(ChildSelector(e)),
                _ => ChildSelectorAsync
            };
        }

        public async Task ExpandAllAsync()
        {
            await ExpandAllAsync(Items);
        }

        public void CollapseAll()
        {
            expandedItems.Clear();
        }

        private async Task ExpandAllAsync(IList<TItem> items)
        {
            foreach (var item in items)
            {
                if (!IsExpanded(item))
                {
                    expandedItems.Add(item);
                }

                await ExpandAllAsync(await ChildSelectorAsync(item));
            }
        }

        private async Task CheckAllAsync(IList<TItem> items, bool setChecked)
        {
            foreach (var item in items)
            {
                if (setChecked)
                {
                    if (!checkedItems.Contains(item))
                    {
                        checkedItems.Add(item);
                    }
                }
                else
                {
                    if (checkedItems.Contains(item))
                    {
                        _ = checkedItems.Remove(item);
                    }
                }

                if (CheckboxMode == CheckboxMode.Recursive)
                {
                    await CheckAllAsync(await ChildSelectorAsync(item), setChecked);
                }
            }
        }

        private async Task SetDefaultExpandedAsync(IList<TItem> items)
        {
            foreach (var item in items)
            {
                if (!IsExpanded(item) && DefaultExpanded(item))
                {
                    expandedItems.Add(item);
                }

                await SetDefaultExpandedAsync(await ChildSelectorAsync(item));
            }
        }

        public bool IsSelected(TItem item)
        {
            return selectedItems.Contains(item);
        }

        public bool? IsChecked(TItem item)
        {
            return checkedItems.Contains(item);
        }

        public bool IsExpanded(TItem item)
        {
            return expandedItems.Contains(item);
        }

        public async void ToggleExpandedAsync(TItem item)
        {
            try
            {
                if (IsExpanded(item))
                {
                    _ = expandedItems.Remove(item);
                }
                else
                {
                    expandedItems.Add(item);
                }

                if (ExpandedItemsChanged.HasDelegate)
                {
                    await ExpandedItemsChanged.InvokeAsync(expandedItems);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task ToggleCheckedAsync(TItem item)
        {
            if (IsChecked(item) == true)
            {
                _ = checkedItems.Remove(item);
                if (CheckboxMode == CheckboxMode.Recursive)
                {
                    await CheckAllAsync(await ChildSelectorAsync(item), false);
                }
            }
            else
            {
                checkedItems.Add(item);
                if (CheckboxMode == CheckboxMode.Recursive)
                {
                    await CheckAllAsync(await ChildSelectorAsync(item), true);
                }
            }

            await CheckedItemsChanged.InvokeAsync(checkedItems);
            StateHasChanged();
        }

        public async Task ToogleSelectedAsync(TItem item)
        {
            var removed = false;
            if (!MultiSelect)
            {
                selectedItems.Clear();
            }

            if (IsSelected(item))
            {
                _ = selectedItems.Remove(item);
                removed = true;
            }
            else
            {
                selectedItems.Add(item);
            }

            if (removed)
            {
                await SelectedItemChanged.InvokeAsync(default);
            }
            else
            {
                await SelectedItemChanged.InvokeAsync(item);
            }

            await SelectedItemsChanged.InvokeAsync(selectedItems);
            await InvokeAsync(StateHasChanged);
        }
    }
}
