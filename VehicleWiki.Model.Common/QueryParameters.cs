namespace VehicleWiki.Model.Common;

public class QueryParameters
{
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? NameFilter { get; set; }
    public string? AbrvFilter { get; set; }
}