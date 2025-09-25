using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Display.Components.TreeViews
{
    public partial class TreeViewNodes<TItem> : BaseComponent
    {
        [CascadingParameter(Name = "Root")]
        private TreeView<TItem> Root { get; set; }
        [Parameter] public RenderFragment<TItem> Template { get; set; }
        [Parameter] public Func<TItem, Task<IList<TItem>>> ChildSelectorAsync { get; set; } = node => null;
        [Parameter] public IList<TItem> Items { get; set; }
        [Parameter] public bool AllowDrop { get; set; }
        [Parameter] public int Level { get; set; }


        private readonly Dictionary<TItem, IList<TItem>> children = new();
        private bool isRoot => Level == 0;

        private bool CheckAllowDrop(TItem item)
        {
            return !AllowDrop ? false : Root.DraggedItem != null && !Root.DraggedItem.Equals(item);
        }

        protected override async Task OnParametersSetAsync()
        {
            children.Clear();
            foreach (TItem item in Items)
            {
                _ = children.TryAdd(item, await ChildSelectorAsync(item));
            }

            StateHasChanged();
        }

        protected IList<TItem> GetChildren(TItem item)
        {
            return !children.TryGetValue(item, out IList<TItem> value) ? new List<TItem>() : value;
        }

        private string GetNodeCss(TItem item)
        {
            string result = "";

            if (Root.DraggedItem != null)
            {
                if (!CheckAllowDrop(item))
                {
                    result += "tree-node-no-drop ";
                }
            }

            return result;
        }

        private string GetContainerCss(TItem item)
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
