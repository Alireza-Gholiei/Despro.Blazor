using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Layout.Components.Buttons
{
    public partial class Button : BaseComponent
    {
        [Parameter] public string Text { get; set; }
        [Parameter] public bool Disabled { get; set; }
        [Parameter] public bool Block { get; set; }
        [Parameter] public bool IsIcon { get; set; }
        [Parameter] public bool IsLoading { get; set; }
        [Parameter] public bool IsDropdown { get; set; }
        [Parameter] public ColorType BackgroundColorType { get; set; } = ColorType.Default;
        [Parameter] public ButtonShape Shape { get; set; } = ButtonShape.Default;
        [Parameter] public ButtonSize Size { get; set; } = ButtonSize.Default;
        [Parameter] public ButtonType Type { get; set; } = ButtonType.Button;
        [Parameter] public string? LinkTo { get; set; }
        [Parameter] public bool LinkToBlank { get; set; }
        [Parameter] public BaseColor TextColor { get; set; } = BaseColor.Default;
        [Parameter] public BaseColor BackgroundColor { get; set; } = BaseColor.Default;
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected string HtmlTag => Type switch
        {
            ButtonType.Input => "input",
            ButtonType.Link => "a",
            ButtonType.Submit => "button",
            ButtonType.Reset => "button",
            _ => "button"
        };

        protected string? InputType => Type switch
        {
            ButtonType.Input => "button",
            ButtonType.Button => "button",
            ButtonType.Submit => "submit",
            ButtonType.Reset => "reset",
            _ => null
        };

        protected string? Href => Type == ButtonType.Link
            ? LinkTo
            : null;

        protected string? HrefBlank => LinkToBlank
            ? "_blank"
            : null;

        protected override string ClassNames => ClassBuilder
                .Add("btn p-lg-2")
                .Add(BackgroundColor.GetColorClass("btn", BackgroundColorType))
                .Add(TextColor.GetColorClass("text"))
                .AddIf("disabled", Disabled)
                .AddIf("btn-block", Block)
                .AddIf("btn-icon", IsIcon)
                .AddIf("btn-loading", IsLoading)
                .AddIf("dropdown-toggle", IsDropdown)
                .AddCompare("btn-pill", Shape, ButtonShape.Pill)
                .AddCompare("btn-square", Shape, ButtonShape.Square)
                .AddCompare("btn-lg", Size, ButtonSize.Large)
                .AddCompare("btn-sm", Size, ButtonSize.Small)
                .ToString();
    }

    public enum ButtonShape
    {
        Default,
        Square,
        Pill
    }

    public enum ButtonSize
    {
        Default,
        Large,
        Small
    }

    public enum ButtonType
    {
        Link,
        Button,
        Input,
        Submit,
        Reset
    }

}