#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.MVC2.Data;
using Project.MVC2.Models;
using Project.Service.Data;
using Project.Service.Interface;
using Project.Service.Repository;

namespace Project.MVC2.Controllers
{
    public class VehicleMakeController : Controller
    {

        private readonly IMakeRepository _repository;
        

        public VehicleMakeController(IMakeRepository repository)
        {
            _repository = repository;

        }

        // GET: VehicleMake
        public async Task <IActionResult> Index()
        {
            IEnumerable<VehicleMake> makes = _repository.GetAllMakes().Select(u => new VehicleMake
            {
                Name = u.Name,
                Abrv = u.Abrv
             
            });
            return View(makes);
        }
    }
}
