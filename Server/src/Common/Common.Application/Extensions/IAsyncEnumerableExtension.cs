namespace Common.Application.Extensions;

public static class AsyncEnumerableExtension
{
    public static IAsyncEnumerable<TSource> TakeIfNotNull<TSource>(this IAsyncEnumerable<TSource> source, int? count)
    {
        if (count.HasValue)
        {
            return source.Take(count.Value);
        }

        return source;
    }

    public static IAsyncEnumerable<TSource> SkipIfNotNull<TSource>(this IAsyncEnumerable<TSource> source, int? count)
    {
        if (count.HasValue)
        {
            return source.Skip(count.Value);
        }

        return source;
    }

    public static IOrderedAsyncEnumerable<TSource> OrderByWidthDescending<TSource, TKey>(this IAsyncEnumerable<TSource> source,
        Func<TSource, TKey> keySelector, bool descending)
    {
        if (descending)
        {
            return source.OrderByDescending(keySelector);
        }
        else
        {
            return source.OrderBy(keySelector);
        }
    }
}