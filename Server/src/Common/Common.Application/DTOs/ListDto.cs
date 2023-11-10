namespace Common.Application.DTOs;

public class ListDto<T> where T: class
{
    public T[] Items { get; set; }
    
    public int TotalCount { get; set; }
}