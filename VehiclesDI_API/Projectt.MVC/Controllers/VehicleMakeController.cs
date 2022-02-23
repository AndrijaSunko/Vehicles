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
using AutoMapper;
using Project.Service.DataTransferObjects;

namespace Project.MVC2.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly IMakeRepository _makeRepository;


        public VehicleMakeController(IMakeRepository makeRepository, ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _makeRepository = makeRepository;
        }

        // GET: VehicleMake
        public async Task <IActionResult> Index(string sortOrder,
                                                string currentFilter,
                                                string searchString)

        {
            try
            {
                ViewData["CurrentSort"] = sortOrder;
                ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewData["AbrvSortParm"] = String.IsNullOrEmpty(sortOrder) ? "abrv_desc" : "";
                ViewData["CurrentFilter"] = searchString;

                // var makes = _repository.GetAllMakes(sortOrder, currentFilter, searchString);
                
                // var makes = from s in _repository.GetAllMakes(sortOrder, currentFilter,searchString) select s;


                  IEnumerable<VehicleMake> makes = (IEnumerable<VehicleMake>)_makeRepository.GetAllMakes
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
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong with the GetAllMakes action {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }

        // GET: VehicleMakes/Details/5
        public async Task<IActionResult> Details(int Id)
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

                return View(make);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with the GetMakeWtihDetails action: {ex.Message}");
                return StatusCode(500, "internal server error");
            }

            
        }
    }
}
