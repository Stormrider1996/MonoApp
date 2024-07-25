using AutoMapper;
using VehicleWiki.Model;
using VehicleWiki.Dtos;

namespace VehicleWiki.WebAPI;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // VehicleMake mappings
        CreateMap<VehicleMake, VehicleMakeDto>();
        CreateMap<VehicleMakeDto, VehicleMake>();
        CreateMap<PagedResult<VehicleMake>, PagedResult<VehicleMakeDto>>();
        CreateMap<PagedResult<VehicleMakeDto>, PagedResult<VehicleMake>>();
        CreateMap<CreateVehicleMakeDto, VehicleMake>();
        CreateMap<UpdateVehicleMakeDto, VehicleMake>();

        //VehicleModel mappings
        CreateMap<VehicleModel, VehicleModelDto>();
        CreateMap<VehicleModelDto, VehicleModel>();
        CreateMap<PagedResult<VehicleModel>, PagedResult<VehicleModelDto>>();
        CreateMap<PagedResult<VehicleModelDto>, PagedResult<VehicleModel>>();
        CreateMap<CreateVehicleModelDto, VehicleModel>();
        CreateMap<UpdateVehicleModelDto, VehicleModel>();
    }
}
