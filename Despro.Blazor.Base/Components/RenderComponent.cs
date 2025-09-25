using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;
using System.Reflection;

namespace Despro.Blazor.Base.Components
{
    public class RenderComponent<TComponent> where TComponent : BaseComponent
    {
        private static readonly Type TComponentType = typeof(TComponent);
        private readonly Dictionary<string, object> parameters = new(StringComparer.Ordinal);

        public RenderComponent<TComponent> Set<TValue>(Expression<Func<TComponent, TValue>> parameterSelector, TValue value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            parameters.Add(GetParameterName(parameterSelector), value);
            return this;
        }

        private static string GetParameterName<TValue>(Expression<Func<TComponent, TValue>> parameterSelector)
        {
            if (parameterSelector is null)
                throw new ArgumentNullException(nameof(parameterSelector));

            if (parameterSelector.Body is not MemberExpression memberExpression ||
                memberExpression.Member is not PropertyInfo propInfoCandidate)
                throw new ArgumentException($"The parameter selector '{parameterSelector}' does not resolve to a public property on the component '{typeof(TComponent)}'.", nameof(parameterSelector));

            PropertyInfo propertyInfo = propInfoCandidate.DeclaringType != TComponentType
                ? TComponentType.GetProperty(propInfoCandidate.Name, propInfoCandidate.PropertyType)
                : propInfoCandidate;

            ParameterAttribute paramAttr = propertyInfo?.GetCustomAttribute<ParameterAttribute>(inherit: true);
            CascadingParameterAttribute cascadingParameterAttribute = propertyInfo?.GetCustomAttribute<CascadingParameterAttribute>();


            return propertyInfo is null || paramAttr is null && cascadingParameterAttribute is null
                ? throw new ArgumentException($"The parameter selector '{parameterSelector}' does not resolve to a public property on the component '{typeof(TComponent)}' with a [Parameter] or [CascadingParameter] attribute.", nameof(parameterSelector))
                : propertyInfo.Name;
        }

        public RenderFragment Contents
        {
            get
            {
                RenderFragment content = new(x =>
                {
                    int seq = 1;
                    x.OpenComponent(seq++, TComponentType);
                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string, object> parameter in parameters)
                            x.AddAttribute(seq++, parameter.Key, parameter.Value);
                    }

                    x.CloseComponent();
                });

                return content;
            }
        }
    }
}
