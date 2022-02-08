using Microsoft.AspNetCore.Mvc;
using Project.Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakesController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;
        public MakesController(IRepositoryWrapper repositoryWrapper)
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

        // GET api/<MakesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MakesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MakesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MakesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
