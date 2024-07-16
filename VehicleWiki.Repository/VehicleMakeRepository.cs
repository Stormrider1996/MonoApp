using VehicleWiki.DAL;
using VehicleWiki.Model;
using VehicleWiki.Repository.Common;

namespace VehicleWiki.Repository;

public class VehicleMakeRepository : GenericRepository<VehicleMake>, IVehicleMakeRepository
{
    public VehicleMakeRepository(VehicleDbContext context) : base(context) { }
}
