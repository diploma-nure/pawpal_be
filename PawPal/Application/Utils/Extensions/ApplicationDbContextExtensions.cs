﻿namespace Application.Utils.Extensions;

public static class ApplicationDbContextExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int? page, int? pageSize)
    {
        if (!page.HasValue || !pageSize.HasValue)
            return query;

        return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
    }
}
