using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Service;
using Project.Service.Models;
using Project.Service.Service;

namespace Project.MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakesController : ControllerBase
    {
        private readonly VehicleMakeService _vehicleMakeService;
        private readonly IRepository<VehicleMake> _VehicleMake;

        public VehicleMakesController(IRepository<VehicleMake> VehicleMake, VehicleMakeService vehicleMakeService)
        {
            _vehicleMakeService = vehicleMakeService;
            _VehicleMake = VehicleMake;
        }
        //Add Make 
        [HttpPost("AddVehicleMake")]
        public async Task<Object> AddMake([FromBody] VehicleMake vehicleMake)
        {
            try
            {
                await _vehicleMakeService.AddVehicleMake(vehicleMake);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        //Delete Make  
        [HttpDelete("DeleteVehicleMake")]
        public bool DeletePerson(string Name)
        {
            try
            {
                _vehicleMakeService.DeleteVehicleMake(Name);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Delete Make  
        [HttpPut("UpdateVehicleMake")]
        public bool UpdatePerson(VehicleMake Object)
        {
            try
            {
                _vehicleMakeService.UpdateVehicleMake(Object);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //GET All Person by Name  
        [HttpGet("GetAllVehicleMakeName")]
        public Object GetAllVehicleMakeName(string Name)
        {
            var data = _vehicleMakeService.GetVehicleByName(Name);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return json;
        }
        //GET All Makes 
        [HttpGet("GetAllMakes")]
        public Object GetAllMakes()
        {
            var data = _vehicleMakeService.GetVehicleMakes();
            var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return json;
        }
    }
}
