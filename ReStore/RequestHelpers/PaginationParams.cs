namespace Re_Store.RequestHelpers;

public class PaginationParams
{
    private const int _maxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSaze = 6;

    public int PageSize
    {
        get => _pageSaze;
        set => _pageSaze = value > _maxPageSize ? _maxPageSize : value;
    }
}