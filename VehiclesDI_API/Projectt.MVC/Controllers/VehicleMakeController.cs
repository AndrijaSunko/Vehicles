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
using Project.Service.Helpers;
using Project.Service.Interface;
using Project.Service.Repository;
using Microsoft.EntityFrameworkCore;

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
        public async Task <IActionResult> Index(string sortOrder,
                                                string currentFilter,
                                                string searchString)

        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = String.IsNullOrEmpty(sortOrder) ? "abrv_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            // var makes = _repository.GetAllMakes(sortOrder, currentFilter, searchString);
           // var makes = from s in _repository.GetAllMakes(sortOrder, currentFilter,searchString) select s;

            
              IEnumerable<VehicleMake> makes = (IEnumerable<VehicleMake>)_repository.GetAllMakes
                                                    (sortOrder,
                                                    currentFilter,
                                                    searchString
                                                    ).Select(u => new VehicleMake
                                                    {
                                                            Name = u.Name,
                                                            Abrv = u.Abrv

                                                    }); 

            return View(makes);
            //  return View(await PaginatedList<VehicleMake>.CreateAsync((IQueryable<VehicleMake>)makes, pageNumber ?? 1, pageSize));
        }
    }
}
