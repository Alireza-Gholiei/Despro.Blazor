using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Display.Components.TreeViews
{
    public partial class TreeViewNodes<TItem> : BaseComponent
    {
        [CascadingParameter(Name = "Root")] private TreeView<TItem> Root { get; set; }
        [Parameter] public RenderFragment<TItem> Template { get; set; }
        [Parameter] public Func<TItem, Task<IList<TItem>>> ChildSelectorAsync { get; set; } = node => null;
        [Parameter] public IList<TItem> Items { get; set; }
        [Parameter] public bool AllowDrop { get; set; }
        [Parameter] public int Level { get; set; }


        private readonly Dictionary<TItem, IList<TItem>> children = new();
        private bool isRoot => Level == 0;

        private bool CheckAllowDrop(TItem item)
        {
            return AllowDrop && (Root.DraggedItem != null && !Root.DraggedItem.Equals(item));
        }

        private IList<TItem> _previousItems = new List<TItem>();
        protected override async Task OnParametersSetAsync()
        {
            //if ((!ReferenceEquals(Items, _previousItems)
            //     || _previousItems.Count != Items.Count)
            //    && Items != null
            //    && Items.Any())
            //{
            //    _previousItems = Items;

            //    children.Clear();
            //    foreach (var item in Items)
            //    {
            //        var childs = await ChildSelectorAsync(item);
            //        children.TryAdd(item, childs);
            //    }
            //}

            if (Items == null && !Items.Any()) return;

            var newItems = Items.Except(_previousItems).ToList();
            foreach (var item in newItems)
            {
                var childs = await ChildSelectorAsync(item);
                children[item] = childs;
            }

            var removedItems = _previousItems.Except(Items).ToList();
            foreach (var item in removedItems)
            {
                children.Remove(item);
            }

            _previousItems = Items.ToList();
        }

        protected IList<TItem> GetChildren(TItem item)
        {
            return !children.TryGetValue(item, out var value)
                ? new List<TItem>()
                : value;
        }

        private string GetNodeCss(TItem item)
        {
            var result = "";

            if (Root.DraggedItem == null) return result;
            if (!CheckAllowDrop(item))
            {
                result += "tree-node-no-drop ";
            }

            return result;
        }

        private static string GetContainerCss(TItem item)
        {
            return "d-flex align-items-center mb-2 tree-container ";
        }

        protected bool HasChildren(TItem item)
        {
            return GetChildren(item)?.Count > 0;
        }

        private async Task DragStart(DragEventArgs e, TItem item)
        {
            if (Root.DraggedItem != null)
            {
                return;
            }

            await Root.SetDraggedAsync(item);
        }

        private async Task DragEnd(DragEventArgs e, TItem item)
        {
            await Root.SetDroppedAsync(default, e);
        }

        private async Task OnDrop(DragEventArgs e, TItem item)
        {
            if (AllowDrop)
            {
                await Root.SetDroppedAsync(item, e);
            }
        }
    }
}
