namespace Resenhando2.Core.Dtos;

public class PagedResultDto<T>(List<T> items, int totalCount)
{
    public List<T> Items { get; set; } = items;
    public int TotalCount { get; set; } = totalCount;
}