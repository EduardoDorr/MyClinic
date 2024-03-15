namespace MyClinic.Common.Models.Pagination;

public class PaginationResult<T>
{
    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages { get; private set; }
    public IReadOnlyList<T> Data { get; private set; } = [];

    public PaginationResult() { }

    public PaginationResult(int page, int pageSize, int totalCount)
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public PaginationResult(int page, int pageSize, int totalCount, int totalPages, List<T> data)
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = totalPages;
        Data = data;
    }

    public void SetTotalPages(int total) => TotalPages = total;
    public void SetData(List<T> values) => Data = values;
}