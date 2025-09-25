using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Layout.Components.Utilities
{
    public partial class VirtualizeOptional<TItem> : BaseComponent
    {
        [Parameter] public IEnumerable<TItem> Items { get; set; }
        [Parameter] public RenderFragment<TItem> ChildContent { get; set; }
        [Parameter] public bool Virtualize { get; set; }
    }
}
