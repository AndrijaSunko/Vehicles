#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.MVC2.Data;
using Project.MVC2.Models;
using Project.Service.DataTransferObjects;
using Project.Service.Interface;
using PagedList;
using ReflectionIT.Mvc.Paging;

namespace Project.MVC2.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        


        public VehicleMakesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            
        }

        // GET: VehicleMake
        public async Task<IActionResult> Index(string sortOrder,
                                                string currentFilter,
                                                string searchString, int pageindex = 1)

        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = String.IsNullOrEmpty(sortOrder) ? "abrv_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            try
            {
         
                IQueryable<VehicleMake> makes = (IQueryable<VehicleMake>)_repository.VehicleMake.GetAllMakes
                                                      (sortOrder,
                                                      currentFilter,
                                                      searchString
                                                      ).Select(u => new VehicleMake
                                                      {
                                                          Name = u.Name,
                                                          Abrv = u.Abrv

                                                      });

              // var model = PaginatedList.CreateAsync(makes, 3, pageIndex)
              var list = PagingList.Create(makes, 3, pageindex);
             
              return View(list);
              
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
                return View(makeResult);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with the GetMakeWtihDetails action: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
            

        }

        // GET: VehicleMakes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleMakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] MakeForCreationDto vehicleMake)
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

                var makeEntity = _mapper.Map<Project.Service.Models.VehicleMake>(vehicleMake);
                _repository.VehicleMake.CreateVehicleMake(makeEntity);
                _repository.save();
                var createdMake = _mapper.Map<VehicleMakeDto>(makeEntity);

              //  return CreatedAtRoute("MakeById", new { id = createdMake.Id }, createdMake);
                return View(createdMake);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with the CreateVehicleMake action: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
           // return View(vehicleMake);
        }

        // GET: VehicleMakes/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var vehicleMake =  _repository.VehicleMake.GetMakeById(Id);
            if (vehicleMake == null)
            {
                return NotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id,Name,Abrv")] MakeForCreationDto vehicleMake)
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

                return View(vehicleMake);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with the UpdateVehicleMake action: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
            
        }

        // GET: VehicleMakes/Delete/5
        public async Task<IActionResult> Delete(int Id)
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
                return View();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with the UpdateVehicleMake action: {ex.Message}");
                return StatusCode(500, "internal server error");
            }


        }

        // POST: VehicleMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            return RedirectToAction(nameof(Index));
        }

       
    }
}
