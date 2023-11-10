namespace Common.Application.DTOs;

public class BaseFilter
{
    public string? FreeText { get; init; }

    public int? Limit { get; init; }
    
    public int? Offset { get; init; }
}