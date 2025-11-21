using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Display.Components.TreeViews
{
    public class ItemDropped<TItem>
    {
        public TItem Item { get; set; }
        public TItem TargetItem { get; set; }
        public DragEventArgs DragEventArgs { get; set; }
    }
}
