namespace InventorySystem.Application.DTOs;

public class PagedResultDto<T>
{
    public int TotalRecords { get; set; }
    public IEnumerable<T> Items { get; set; }
}