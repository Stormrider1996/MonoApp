using Microsoft.EntityFrameworkCore;
using VehicleWiki.DAL;
using VehicleWiki.Dtos;
using VehicleWiki.Model;
using VehicleWiki.Model.Common;
using VehicleWiki.Repository.Common;

namespace VehicleWiki.Repository;

public class VehicleMakeRepository : GenericRepository<VehicleMake>, IVehicleMakeRepository
{
    public VehicleMakeRepository(VehicleDbContext context) : base(context) { }

    public async Task<PagedResult<VehicleMake>> GetPagedAsync(QueryParameters queryParams)
    {
        IQueryable<VehicleMake> query = _context.Set<VehicleMake>();

        // Apply filtering
        if (!string.IsNullOrEmpty(queryParams.NameFilter))
        {
            query = query.Where(v => v.Name.Contains(queryParams.NameFilter));
        }

        if (!string.IsNullOrEmpty(queryParams.AbrvFilter))
        {
            query = query.Where(v => v.Abrv.Contains(queryParams.AbrvFilter));
        }

        // Apply sorting
        if (!string.IsNullOrEmpty(queryParams.SortBy))
        {
            query = queryParams.SortBy.ToLower() switch
            {
                "name" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(v => v.Name)
                    : query.OrderBy(v => v.Name),
                "abrv" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(v => v.Abrv)
                    : query.OrderBy(v => v.Abrv),
                _ => query // Default no sorting
            };
        }

        // Apply paging
        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .ToListAsync();

        return new PagedResult<VehicleMake>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = queryParams.PageNumber,
            PageSize = queryParams.PageSize
        };
    }
}
