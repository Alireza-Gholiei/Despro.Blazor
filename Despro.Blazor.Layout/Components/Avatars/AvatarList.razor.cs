using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Layout.Components.Avatars
{
    public partial class AvatarList : BaseComponent
    {
        [Parameter] public bool Stacked { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }


        protected override string ClassNames => ClassBuilder
            .Add("avatar-list")
            .AddIf("avatar-list-stacked", Stacked)
           .ToString();
    }
}