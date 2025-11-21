namespace Despro.Blazor.Base.Models;

public class FilterParam
{
    public FilterParam(string Key, string Value)
    {
        this.Key = Key;
        this.Value = Value;
    }

    public void SetOperator(string @operator)
    {
        Operator = @operator;
    }

    public void SetExpression(bool searchExpression)
    {
        SearchExpression = searchExpression;
    }

    public void SetOperatorExpression(string @operator, bool searchExpression)
    {
        Operator = @operator;
        SearchExpression = searchExpression;
    }

    public string Key { get; set; }
    public string Value { get; set; }
    public string Operator { get; set; }
    public bool SearchExpression { get; set; }
}

public enum OrderType
{
    Ascending,
    Descending,
}

public class BaseGrid
{
    public BaseGrid()
    {

    }

    protected BaseGrid(int currentPage, int limit, string orderField, OrderType orderType, List<FilterParam> filterParam)
    {
        CurrentPage = currentPage;
        Limit = limit;
        OrderField = orderField;
        OrderType = orderType;
        FilterParam = filterParam;
    }

    public int CurrentPage { get; set; } = 1;
    public int Limit { get; set; } = 10;
    public string OrderField { get; set; } = "Id";
    public OrderType OrderType { get; set; } = OrderType.Descending;
    public List<FilterParam> FilterParam { get; set; } = [];


    public void Set(int currentPage, int limit, string orderField, OrderType orderType, List<FilterParam> filterParam)
    {
        CurrentPage = currentPage;
        Limit = limit;
        OrderField = orderField;
        OrderType = orderType;
        FilterParam = filterParam;
    }
}

public class GridData<TData> : BaseGrid
{
    public GridData()
    {

    }

    public GridData(BaseGrid baseGrid, List<TData> data, int entityCount, int pageCount)
        : base(baseGrid.CurrentPage, baseGrid.Limit, baseGrid.OrderField, baseGrid.OrderType, baseGrid.FilterParam)
    {
        Data = data;
        EntityCount = entityCount;
        PageCount = pageCount;
    }

    public List<TData> Data { get; set; }
    public int EntityCount { get; set; }
    public int PageCount { get; set; }
}