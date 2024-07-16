using VehicleWiki.DAL;
using VehicleWiki.Repository.Common;

namespace VehicleWiki.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly VehicleDbContext _context;
    public IVehicleMakeRepository VehicleMakes { get; private set; }
    public IVehicleModelRepository VehicleModels { get; private set; }

    public UnitOfWork(VehicleDbContext context)
    {
        _context = context;
        VehicleMakes = new VehicleMakeRepository(_context);
        VehicleModels = new VehicleModelRepository(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

}
