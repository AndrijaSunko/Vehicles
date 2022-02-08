using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Service.DataTransferObjects;
using Project.Service.Interface;
using Project.Service.Models;
using Project.Service.Repository;

namespace Project.Api.Controllers
{
    [Route("api/Makes")]
    [ApiController]
    public class MakesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public MakesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger= logger;
            _repository= repository;
            _mapper= mapper;
        }


        [HttpGet]
        public IActionResult GetAllMakes([FromQuery] MakeParams makeParams)
        {
            try
            {
                
                var makes = _repository.VehicleMake.GetAllMakes(makeParams);

                var metadata = new
                {
                    makes.TotalCount,
                    makes.PageSize,
                    makes.CurrentPage,
                    makes.HasNext,
                    makes.HasPrevious             
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                _logger.LogInfo($"Returned {makes.TotalCount} vehicle makes from the database.");

                var makesResult = _mapper.Map<IEnumerable<VehicleMakeDto>>(makes);
                return Ok(makesResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong with the GetAllMakes action {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }

        [HttpGet("{Id}", Name ="MakeById")]
        public IActionResult GetMakeById(int Id)
        {
            try
            {
                var make = _repository.VehicleMake.GetMakeById(Id);
                
                if (make == null)
                {
                    _logger.LogError($"Make with  Id: {Id}, was not found in the database");
                    return NotFound();
                }
                _logger.LogInfo($"Returned with Id: {Id}");

                var makeResult = _mapper.Map<VehicleMakeDto>(make);
                return Ok(makeResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong with the GetMakeById action {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }
        [HttpGet("{Id}/VehicleModel")]
        public IActionResult GetMakeWithDetails(int Id)
        {
            try
            {
                var make = _repository.VehicleMake.GetMakeWithDetails(Id);
                if (make == null)
                {
                    _logger.LogError($"Make with  Id: {Id}, was not found in the database");
                    return NotFound();
                }
                _logger.LogInfo($"Returned make with details with Id: {Id}");

                var makeResult = _mapper.Map<VehicleMakeDto>(make);
                return Ok(makeResult);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with the GetMakeWtihDetails action: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateVehicleMake ([FromBody]MakeForCreationDto vehicleMake)
        {
            try
            {             
                if (vehicleMake == null)
                {
                    _logger.LogError("Make object sent from the client is null");
                    return BadRequest("Make object is null");
                }
                if (ModelState.IsValid)
                {
                    _logger.LogError("Make object sent from the client is invalid");
                    return BadRequest("Make object is invalid");
                }

                var makeEntity =  _mapper.Map<VehicleMake>(vehicleMake);
                _repository.VehicleMake.CreateVehicleMake(makeEntity);
                _repository.save();
                var createdMake = _mapper.Map<VehicleMakeDto>(makeEntity);

                return CreatedAtRoute("MakeById", new { id = createdMake.Id}, createdMake);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with the CreateVehicleMake action: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateVehicleMake (int Id, [FromBody]MakeForCreationDto vehicleMake)
        {
            try
            {
                if (vehicleMake == null)
                {
                    _logger.LogError("Make object sent from the client is null");
                    return BadRequest("Make object is null");
                }
                if (ModelState.IsValid)
                {
                    _logger.LogError("Make object sent from the client is invalid");
                    return BadRequest("Make object is invalid");
                }
                
                var makeEntity = _repository.VehicleMake.GetMakeById(Id);
                if (makeEntity == null)
                {
                    _logger.LogError($"Make with  Id: {Id}, was not found in the database");
                    return NotFound();
                }
                _mapper.Map(vehicleMake, makeEntity);
                _repository.VehicleMake.UpdateVehicleMake(makeEntity);
                _repository.save();

                return NoContent();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with the UpdateVehicleMake action: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }

        [HttpDelete("{Id}")]

        public IActionResult DeleteVehicleMake(int Id)
        {
            try
            {
                var make = _repository.VehicleMake.GetMakeById(Id);
                if (make == null)
                {
                    _logger.LogError($"Make with  Id: {Id}, was not found in the database");
                    return NotFound();
                }

                _repository.VehicleMake.DeleteVehicleMake(make);
                _repository.save();
                return NoContent();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with the UpdateVehicleMake action: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }

    }
}
