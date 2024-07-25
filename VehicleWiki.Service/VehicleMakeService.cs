using AutoMapper;
using VehicleWiki.Dtos;
using VehicleWiki.Model;
using VehicleWiki.Repository.Common;
using VehicleWiki.Service.Common;
using VehicleWiki.Common.NotFoundException;
using VehicleWiki.Model.Common;

namespace VehicleWiki.Service;

public class VehicleMakeService : IVehicleMakeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VehicleMakeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<VehicleMakeDto>> GetAllVehicleMakesAsync()
    {
        var vehicleMakes = await _unitOfWork.VehicleMakes.GetAllAsync();
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<IEnumerable<VehicleMakeDto>>(vehicleMakes);
    }
    public async Task<VehicleMakeDto> GetVehicleMakeByIdAsync(Guid id)
    {
        var vehicleMake = await _unitOfWork.VehicleMakes.GetByIdAsync(id);
        if (vehicleMake == null) throw new NotFoundException("Vehicle Make not found");
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<VehicleMakeDto>(vehicleMake);
    }

    public async Task<PagedResult<VehicleMakeDto>> GetPagedVehicleMakesAsync(QueryParameters queryParams)
    {
        var paged = await _unitOfWork.VehicleMakes.GetPagedAsync(queryParams);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<PagedResult<VehicleMakeDto>>(paged);
    }

    public async Task<VehicleMakeDto> CreateVehicleMakeAsync(CreateVehicleMakeDto vehicleMakeDto)
    {
        var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeDto);
        await _unitOfWork.VehicleMakes.AddAsync(vehicleMake);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<VehicleMakeDto>(vehicleMake);
    }
    public async Task<VehicleMakeDto> UpdateVehicleMakeAsync(UpdateVehicleMakeDto updateDto, Guid vehicleMakeId)
    {
        var vehicleMake = await _unitOfWork.VehicleMakes.GetByIdAsync(vehicleMakeId);
        if (vehicleMake == null) throw new NotFoundException("Vehicle Make not found");
        _mapper.Map(updateDto, vehicleMake);
        _unitOfWork.VehicleMakes.Update(vehicleMake);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<VehicleMakeDto>(vehicleMake);
    }

    public async Task DeleteVehicleMakeAsync(Guid id)
    {
        var vehicleMake = await _unitOfWork.VehicleMakes.GetByIdAsync(id);
        if (vehicleMake == null) throw new NotFoundException("Vehicle Make not found");
        _unitOfWork.VehicleMakes.Remove(vehicleMake);
        await _unitOfWork.CompleteAsync();
    }
}