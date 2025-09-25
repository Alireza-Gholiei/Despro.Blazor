using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Layout.Components.Navigation
{
    public abstract class NavigationBase : BaseComponent, IDisposable
    {
        [CascadingParameter] public NavigationBase Parent { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        internal List<NavigationBase> Children { get; set; } = new();
        public NavigationItem SelectedItem { get; set; }

        internal bool IsExpanded;
        internal bool IsActive;
        internal bool ExpandClick;
        public bool Disabled { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Parent?.AddChildItem(this);
        }

        public void AddChildItem(NavigationBase child)
        {
            child.ExpandClick = ExpandClick;
            Children.Add(child);
        }

        public void RemoveChildItem(NavigationBase child)
        {
            _ = Children.Remove(child);
        }

        public virtual void ChildSelected(NavigationItem child)
        {
            SelectedItem = child;
            SetActive(true);
            Parent?.ChildSelected(child);
        }

        public void SetExpanded(bool expanded)
        {
            IsExpanded = expanded;
        }

        public void CollapseAll()
        {

            if (Parent != null)
            {
                Parent.CollapseAll();
            }
            else
            {
                foreach (NavigationBase child in Children)
                {

                    child.SetExpanded(false);
                }
            }

        }

        public void SetActive(bool active)
        {
            IsActive = active;

            if (Parent != null)
            {
                if (active)
                {
                    foreach (NavigationBase child in Parent.Children)
                    {
                        if (child != this)
                        {
                            child.SetActive(false);
                            child.SetExpanded(false);
                        }
                    }
                }
            }

            StateHasChanged();
        }

        public void Dispose()
        {
            Parent?.RemoveChildItem(this);
        }
    }
}
