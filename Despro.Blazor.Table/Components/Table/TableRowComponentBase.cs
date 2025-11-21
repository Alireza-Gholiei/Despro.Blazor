using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Despro.Blazor.Table.TableGenerals.Table;
using Despro.Blazor.Table.TableRepository.Interface.Table;

namespace Despro.Blazor.Table.Components.Table
{
    public abstract class TableRowComponentBase<TableItem> : BaseComponent
    {
        public string? GetColumnWidth(IColumn<TableItem> column)
        {
            string? style = !string.IsNullOrEmpty(column.Width) ? $"width:{column.Width}; " : null;

            return style;
        }

        public virtual string GetColumnClass(IColumn<TableItem> column)
        {
            string classes = new ClassBuilder()
                .Add(column.CssClass)
                .AddIf("text-end", column.Align == Align.End)
                .ToString();

            return classes;
        }
    }
}