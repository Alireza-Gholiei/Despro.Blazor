using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Display.Components.Statuses
{

    public partial class Status : BaseComponent
    {
        [Parameter] public bool Lite { get; set; }
        [Parameter] public StatusDotType DotType { get; set; } = StatusDotType.None;
        [Parameter] public BaseColor TextColor { get; set; } = BaseColor.Default;
        [Parameter] public BaseColor BackgroundColor { get; set; } = BaseColor.Default;
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected override string ClassNames => ClassBuilder
            .Add("status")
            .Add(BackgroundColor.GetColorClass("status", ColorType.Default))
            .AddIf(TextColor.GetColorClass("text", ColorType.Default), TextColor != BaseColor.Default)
            .AddIf("status-lite", Lite)
            .AddIf("cursor-pointer", OnClick.HasDelegate)
            .ToString();
    }

    public enum StatusDotType
    {
        None = 0,
        Normal = 1,
        Animate = 2
    }
}
