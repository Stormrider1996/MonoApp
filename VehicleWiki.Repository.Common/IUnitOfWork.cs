namespace VehicleWiki.Repository.Common;

public interface IUnitOfWork : IDisposable
{
    IVehicleMakeRepository VehicleMakes { get; }
    IVehicleModelRepository VehicleModels { get; }
    Task<int> CompleteAsync();
}
