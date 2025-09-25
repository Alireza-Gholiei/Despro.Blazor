namespace Despro.Blazor.Base.DesproBlazorExceptions
{
    public abstract class BaseFrameworkException : Exception
    {
        protected BaseFrameworkException(string message) : base(message)
        {

        }
    }
}
