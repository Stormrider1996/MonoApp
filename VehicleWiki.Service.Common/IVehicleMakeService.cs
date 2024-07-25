using VehicleWiki.Dtos;
using VehicleWiki.Model.Common;

namespace VehicleWiki.Service.Common;

public interface IVehicleMakeService
{
    Task<IEnumerable<VehicleMakeDto>> GetAllVehicleMakesAsync();
    Task<VehicleMakeDto> GetVehicleMakeByIdAsync(Guid id);
    Task<VehicleMakeDto> CreateVehicleMakeAsync(CreateVehicleMakeDto vehicleMake);
    Task<VehicleMakeDto> UpdateVehicleMakeAsync(UpdateVehicleMakeDto vehicleMake, Guid vehicleMakeId);
    Task DeleteVehicleMakeAsync(Guid id);
    Task<PagedResult<VehicleMakeDto>> GetPagedVehicleMakesAsync(QueryParameters queryParams);
}