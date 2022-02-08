using AutoMapper;
using Project.Service.DataTransferObjects;
using Project.Service.Models;

namespace Project.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleMake, VehicleMakeDto>();
            CreateMap<VehicleModel, VehicleModelDto>();
        }
    }
}
