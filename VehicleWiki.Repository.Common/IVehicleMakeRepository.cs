using VehicleWiki.Dtos;
using VehicleWiki.Model;
using VehicleWiki.Model.Common;

namespace VehicleWiki.Repository.Common;

public interface IVehicleMakeRepository : IGenericRepository<VehicleMake>
{
    Task<PagedResult<VehicleMake>> GetPagedAsync(QueryParameters queryParams);
}
