namespace Common.Application.DTOs;

public class BaseOrderedFilter<TOrderEnum> : BaseFilter where TOrderEnum: Enum
{
    public TOrderEnum? OrderBy { get; init; }
    
    public bool? Descending { get; init; }
}