using AutoMapper;
using VehicleWiki.Common.NotFoundException;
using VehicleWiki.Dtos;
using VehicleWiki.Model;
using VehicleWiki.Model.Common;
using VehicleWiki.Repository.Common;
using VehicleWiki.Service.Common;

namespace VehicleWiki.Service;

public class VehicleModelService : IVehicleModelService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VehicleModelService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VehicleModelDto>> GetAllVehicleModelsAsync()
    {
        var vehicleModels = await _unitOfWork.VehicleModels.GetAllAsync();
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<IEnumerable<VehicleModelDto>>(vehicleModels);
    }

    public async Task<VehicleModelDto> GetVehicleModelByIdAsync(Guid id)
    {
        var vehicleModel = await _unitOfWork.VehicleModels.GetByIdAsync(id);
        if (vehicleModel == null) throw new NotFoundException("Vehicle Model not found");
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<VehicleModelDto>(vehicleModel);
    }

    public async Task<IEnumerable<VehicleModelDto>> GetVehicleModelsByMakeIdAsync(Guid makeId)
    {
        var vehicleModels = await _unitOfWork.VehicleModels.GetByMakeIdAsync(makeId);
        if (vehicleModels == null) throw new NotFoundException("Vehicle Models not found");
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<IEnumerable<VehicleModelDto>>(vehicleModels);
    }

    public async Task<PagedResult<VehicleModelDto>> GetPagedVehicleModelsAsync(QueryParameters queryParams)
    {
        var paged = await _unitOfWork.VehicleMakes.GetPagedAsync(queryParams);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<PagedResult<VehicleModelDto>>(paged);
    }

    public async Task<VehicleModelDto> CreateVehicleModelAsync(CreateVehicleModelDto vehicleModelDto)
    {
        var vehicleModel = _mapper.Map<VehicleModel>(vehicleModelDto);
        await _unitOfWork.VehicleModels.AddAsync(vehicleModel);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<VehicleModelDto>(vehicleModel);
    }

    public async Task<VehicleModelDto> UpdateVehicleModelAsync(UpdateVehicleModelDto updateDto, Guid vehicleModelId)
    {
        var vehicleModel = await _unitOfWork.VehicleModels.GetByIdAsync(vehicleModelId);
        if (vehicleModel == null) throw new NotFoundException("Vehicle Model not found");
        _mapper.Map(updateDto, vehicleModel);
        _unitOfWork.VehicleModels.Update(vehicleModel);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<VehicleModelDto>(vehicleModel);
    }

    public async Task DeleteVehicleModelAsync(Guid id)
    {
        var vehicleModel = await _unitOfWork.VehicleModels.GetByIdAsync(id);
        if (vehicleModel == null) throw new NotFoundException("Vehicle Model not found");
        _unitOfWork.VehicleModels.Remove(vehicleModel);
        await _unitOfWork.CompleteAsync();
    }
}
