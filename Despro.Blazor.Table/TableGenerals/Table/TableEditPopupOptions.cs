namespace Despro.Blazor.Table.TableGenerals.Table
{
    public class TableEditPopupOptions<TItem>
    {
        public string Title { get; set; }
        public TItem CurrentEditItem { get; internal set; }
        public bool IsAddInProgress { get; internal set; }
    }
}
