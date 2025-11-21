using Despro.Blazor.Base.Components;
using Despro.Blazor.Table.TableGenerals.Table;
using Despro.Blazor.Table.TableRepository.Interface.Table;
using Despro.Blazor.Table.TableRepository.Service.Table;
using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace Despro.Blazor.Table.Components.Table
{
    public class ColumnBase<TItem> : BaseComponent, IColumn<TItem>
    {
        //[Inject] protected TableFilterService FilterService { get; set; }

        [CascadingParameter(Name = "Table")] public ITable<TItem> Table { get; set; } = null!;
        private string? _title;
        [Parameter]
        public string Title
        {
            get
            {
                var title = _title ?? Property.GetPropertyMemberInfo().Name;

                return title;
            }
            set => _title = value;
        }
        [Parameter] public string CssClass { get; set; } = "";
        [Parameter] public string Width { get; set; } = "";
        [Parameter] public bool ActionColumn { get; set; } = false;
        [Parameter] public bool Searchable { get; set; } = false;
        [Parameter] public bool Groupable { get; set; } = false;
        [Parameter] public bool Sortable { get; set; } = false;
        [Parameter] public bool Visible { get; set; } = true;
        [Parameter] public bool Group { get; set; } = false;
        [Parameter] public RenderFragment<TableResult<object, TItem>> GroupingTemplate { get; set; } = null!;
        [Parameter] public RenderFragment<TItem> EditorTemplate { get; set; } = null!;
        [Parameter] public RenderFragment<TItem> Template { get; set; } = null!;
        [Parameter] public RenderFragment HeaderTemplate { get; set; } = null!;
        [Parameter] public Expression<Func<TItem, object>> Property { get; set; } = null!;
        [Parameter] public string? SearchExpression { get; set; }
        //[Parameter] public Expression<Func<TItem, string, bool>> SearchExpression { get; set; } = null!;
        [Parameter] public SortOrder? Sort { get; set; }
        [Parameter] public Align Align { get; set; }


        public bool SortColumn { get; set; }
        public bool GroupBy { get; set; }
        public bool SortDescending { get; set; }
        public Type Type { get; private set; }
        public string SearchText { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                GroupBy = Group;

                if (Sort != null)
                {
                    SortColumn = true;
                    SortDescending = Sort == SortOrder.Descending;
                }

                Table.AddColumn(this);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected override void OnParametersSet()
        {
            try
            {
                if (Sortable && Property == null || Searchable && Property == null)
                {
                    throw new InvalidOperationException($"ستون {Title} خالی است");
                }

                if (Title == null && Property == null)
                {
                    throw new InvalidOperationException("یک ستون دارای هر دو پارامتر عنوان و ویژگی خالی است");
                }

                if (Property == null) return;

                var memberInfo = Property.GetPropertyMemberInfo();

                var underlyingType = memberInfo.GetMemberUnderlyingType();

                Type = underlyingType;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Dispose()
        {
            try
            {
                Table.RemoveColumn(this);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private Expression<Func<TItem, bool>> NotNull()
        {
            try
            {
                return Expression.Lambda<Func<TItem, bool>>(
                    Expression.NotEqual(Property.Body, Expression.Constant(null)),
                    Property.Parameters.ToArray()
                );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task GroupByMeAsync()
        {
            try
            {
                if (Groupable)
                {
                    if (GroupBy)
                    {
                        GroupBy = false;
                        Visible = true;
                    }
                    else
                    {
                        foreach (var column in Table.Columns.Where(e => e.GroupBy))
                        {
                            column.GroupBy = false;
                            column.Visible = true;
                        }
                        GroupBy = true;
                        Visible = false;
                    }

                    await Table.Update(true);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task SortByAsync()
        {
            try
            {
                if (Sortable)
                {
                    var sortOnColumn = true;
                    if (SortColumn)
                    {
                        if (SortDescending && Table.ResetSortCycle)
                        {
                            sortOnColumn = false;
                        }
                        SortDescending = !SortDescending;
                    }

                    Table.Columns.ForEach(x => x.SortColumn = false);

                    SortColumn = sortOnColumn;
                    await Table.Update();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public object GetValue(TItem item)
        {
            try
            {
                return Property.Compile().Invoke(item);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}