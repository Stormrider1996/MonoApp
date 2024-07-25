using Microsoft.EntityFrameworkCore;
using VehicleWiki.DAL;
using VehicleWiki.Dtos;
using VehicleWiki.Model;
using VehicleWiki.Model.Common;
using VehicleWiki.Repository.Common;

namespace VehicleWiki.Repository;

public class VehicleModelRepository : GenericRepository<VehicleModel>, IVehicleModelRepository
{
    public VehicleModelRepository(VehicleDbContext context) : base(context) { }

    public async Task<IEnumerable<VehicleModel>> GetByMakeIdAsync(Guid makeId)
    {
        return await _context.Set<VehicleModel>().Where(m => m.MakeId == makeId).ToListAsync();
    }

    public async Task<PagedResult<VehicleModel>> GetPagedAsync(QueryParameters queryParams)
    {
        IQueryable<VehicleModel> query = _context.Set<VehicleModel>();

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

        return new PagedResult<VehicleModel>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = queryParams.PageNumber,
            PageSize = queryParams.PageSize
        };
    }
}
