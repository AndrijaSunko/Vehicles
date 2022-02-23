using AutoMapper;
using Project.Service.DataTransferObjects;
using Project.Service.Models;

namespace Project.MVC2
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleMake, VehicleMakeDto>();
            CreateMap<VehicleModel, VehicleModelDto>();
            CreateMap<MakeForCreationDto, Project.MVC2.Models.VehicleMake>();
            CreateMap<MakeForUpdateDto, VehicleMake>();
           
        }
    }
}
