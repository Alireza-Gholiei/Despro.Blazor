using Despro.Blazor.Base.BaseGenerals;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Base.Components;

public abstract class BaseComponent : ComponentBase, IComponent, IHandleEvent, IHandleAfterRender
{
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> UnmatchedParameters { get; set; } = new Dictionary<string, object>();

    protected ClassBuilder ClassBuilder => new(ProvidedCssClasses);

    protected string ProvidedCssClasses
    {
        get
        {
            var cssClasses = GetUnmatchedParameter("class")?.ToString();

            if (cssClasses != null)
            {
                field = cssClasses;
            }

            if (field != null)
            {

            }

            return field;
        }
    }

    protected virtual string ClassNames => ClassBuilder.ToString();

    protected object GetUnmatchedParameter(string key)
    {
        if (!(UnmatchedParameters?.ContainsKey(key) ?? false)) return null;

        var value = UnmatchedParameters[key];
        _ = UnmatchedParameters.Remove(key);
        return value;
    }
}