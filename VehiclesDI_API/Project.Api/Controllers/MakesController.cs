using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Service.DataTransferObjects;
using Project.Service.Interface;

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
        public IActionResult GetAllMakes()
        {
            try
            {
                var makes = _repository.VehicleMake.GetAllMakes();

                _logger.LogInfo($"Returned all vehicle makes");

                var makesResult = _mapper.Map<IEnumerable<VehicleMakeDto>>(makes);
                return Ok(makesResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong with the GetAllMakes action {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }

        [HttpGet("{Id}")]
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
            
    }
}
