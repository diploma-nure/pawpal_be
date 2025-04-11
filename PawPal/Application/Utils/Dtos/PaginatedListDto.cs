namespace Application.Utils.Dtos;

public class PaginatedListDto<TData>
{
    public List<TData> Items { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int Count { get; set; }
}
