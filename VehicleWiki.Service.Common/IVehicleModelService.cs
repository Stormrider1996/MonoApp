using VehicleWiki.Dtos;
using VehicleWiki.Model.Common;

namespace VehicleWiki.Service.Common;

public interface IVehicleModelService
{
    Task<IEnumerable<VehicleModelDto>> GetAllVehicleModelsAsync();
    Task<VehicleModelDto> GetVehicleModelByIdAsync(Guid id);
    Task<IEnumerable<VehicleModelDto>> GetVehicleModelsByMakeIdAsync(Guid id);
    Task<VehicleModelDto> CreateVehicleModelAsync(CreateVehicleModelDto vehicleModel);
    Task<VehicleModelDto> UpdateVehicleModelAsync(UpdateVehicleModelDto updateDto, Guid vehicleModelId);
    Task DeleteVehicleModelAsync(Guid id);
    Task<PagedResult<VehicleModelDto>> GetPagedVehicleModelsAsync(QueryParameters queryParams);
}
