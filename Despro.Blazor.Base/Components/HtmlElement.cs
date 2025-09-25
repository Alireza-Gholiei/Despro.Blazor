﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Despro.Blazor.Base.Components
{
    public class HtmlElement : BaseComponent
    {
        [Parameter] public string Tag { get; set; }
        [Parameter] public ElementReference ElementRef { get; set; }
        [Parameter] public Action<ElementReference> ElementRefChanged { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }


        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            try
            {
                base.BuildRenderTree(builder);

                int seq = 0;

                UnmatchedParameters = UnmatchedParameters.Where(x => x.Value != null).ToDictionary();

                builder.OpenElement(seq++, Tag);
                builder.AddMultipleAttributes(seq++, UnmatchedParameters);
                builder.AddElementReferenceCapture(seq++, reference =>
                {
                    ElementRef = reference;
                    ElementRefChanged?.Invoke(ElementRef);
                });
                builder.AddContent(seq++, ChildContent);
                builder.CloseElement();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}