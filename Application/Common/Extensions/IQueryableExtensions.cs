using Microsoft.EntityFrameworkCore;

namespace Application.Common.Extensions
{
    internal static class IQueryableExtensions
    {
        public static async Task<PaginatedList<TDestination>> PaginateAsync<TDestination>(this IQueryable<TDestination> query,int pageNumber, int pageSize, CancellationToken cancellationToken = default) where TDestination: class
        {
            var totalCount = await query.CountAsync(cancellationToken);
            var data = await query.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PaginatedList<TDestination>(data, totalCount, pageNumber, pageSize);
        }
    }
}
