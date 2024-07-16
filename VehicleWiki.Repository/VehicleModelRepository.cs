using VehicleWiki.DAL;
using VehicleWiki.Model;
using VehicleWiki.Repository.Common;

namespace VehicleWiki.Repository;

public class VehicleModelRepository : GenericRepository<VehicleModel>, IVehicleModelRepository
{
    public VehicleModelRepository(VehicleDbContext context) : base(context) { }
}
