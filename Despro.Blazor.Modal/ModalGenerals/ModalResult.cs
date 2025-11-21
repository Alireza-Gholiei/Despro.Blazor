namespace Despro.Blazor.Modal.ModalGenerals
{
    public class ModalResult
    {
        internal ModalResult(object data, Type resultType, bool cancelled)
        {
            Data = data;
            DataType = resultType;
            Cancelled = cancelled;
        }

        public object Data { get; }
        public Type DataType { get; }
        public bool Cancelled { get; }

        public static ModalResult Ok()
        {
            return new ModalResult(default, typeof(object), false);
        }

        public static ModalResult Ok<T>(T result)
        {
            return new ModalResult(result, typeof(T), false);
        }

        public static ModalResult Cancel()
        {
            return new ModalResult(default, typeof(object), true);
        }
    }
}
