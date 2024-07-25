using VehicleWiki.Model;

namespace VehicleWiki.Repository.Common;

public interface IVehicleModelRepository : IGenericRepository<VehicleModel>
{
    Task<IEnumerable<VehicleModel>> GetByMakeIdAsync(Guid makeId);
}
