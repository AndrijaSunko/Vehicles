using Microsoft.AspNetCore.Mvc;
using Project.Service.Interface;

namespace Project.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;
        public WeatherForecastController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        // GET: api/<MakesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var makes = _repositoryWrapper.VehicleMake.FindAll();
            return new string[] { "value1", "value2" };
        }

    }
}