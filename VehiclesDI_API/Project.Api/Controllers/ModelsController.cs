using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Service.DataTransferObjects;
using Project.Service.Interface;
using Project.Service.Models;

namespace Project.Api.Controllers
{
    [Route("api/models")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public ModelsController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllModels([FromQuery] ModelParams modelParams)
        {
            try
            {

                var models = _repository.VehicleModel.GetAllModels(modelParams);

                var metadata = new
                {
                    models.TotalCount,
                    models.PageSize,
                    models.CurrentPage,
                    models.HasNext,
                    models.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                _logger.LogInfo($"Returned {models.TotalCount} vehicle models from the database.");

                var modelsResult = _mapper.Map<IEnumerable<VehicleModelDto>>(models);
                return Ok(modelsResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong with the GetAllModels action {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }
    }
}
