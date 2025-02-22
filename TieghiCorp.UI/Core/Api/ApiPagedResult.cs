namespace TieghiCorp.UI.Core.Api;

public class ApiPagedResult<TData>
{
    public int TotalCount { get; set; }
    public IEnumerable<TData> Data { get; set; } = [];
}