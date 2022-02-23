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
using ReflectionIT.Mvc.Paging;

namespace Project.MVC2.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        


        public VehicleModelsController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
          
        }

        // GET: VehicleModels
        
        public async Task<IActionResult> Index(string sortOrder,
                                                 string currentFilter,
                                                 string searchString, int pageindex = 1)

            {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = String.IsNullOrEmpty(sortOrder) ? "abrv_desc" : "";
            ViewData["MakeIdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "makeId_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            try
                {
                    
                    IQueryable<VehicleModel> models = (IQueryable<VehicleModel>)_repository.VehicleModel.GetAllModels
                                                          (sortOrder,
                                                          currentFilter,
                                                          searchString
                                                          ).Select(u => new VehicleModel
                                                          {
                                                              MakeId = u.MakeId,
                                                              Name = u.Name,
                                                              Abrv = u.Abrv

                                                          });
                var vju = PagingList.Create(models, 3, pageindex);
                return View(vju);
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Something went wrong with the GetAllMakes action {ex.Message}");
                    return StatusCode(500, "internal server error");
                }
            }


        // GET: VehicleModels/Details/5

        // GET: VehicleModels/Details/5
        public async Task<IActionResult> Details(int Id)
        {
            try
            {
                var model = _repository.VehicleModel.GetModelById(Id);
                if (model == null)
                {
                    _logger.LogError($"Make with  Id: {Id}, was not found in the database");
                    return NotFound();
                }
                _logger.LogInfo($"Returned make with details with Id: {Id}");

                var modelResult = _mapper.Map<VehicleMakeDto>(model);
                return View(modelResult);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with the GetMakeWtihDetails action: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }

        // GET: VehicleModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            try
            {
                if (vehicleModel == null)
                {
                    _logger.LogError("Make object sent from the client is null");
                    return BadRequest("Make object is null");
                }
                if (ModelState.IsValid)
                {
                    _logger.LogError("Make object sent from the client is invalid");
                    return BadRequest("Make object is invalid");
                }

                var makeEntity = _mapper.Map<Project.Service.Models.VehicleMake>(vehicleModel);
                _repository.VehicleMake.CreateVehicleMake(makeEntity);
                _repository.save();
                var createdModel = _mapper.Map<VehicleModelDto>(makeEntity);

                //  return CreatedAtRoute("MakeById", new { id = createdMake.Id }, createdMake);
                return View(createdModel);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with the CreateVehicleMake action: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }

        // GET: VehicleModels/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var vehicleModel = _repository.VehicleModel.GetModelById(Id);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            return View(vehicleModel);
        }

        // POST: VehicleModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id,MakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            try
            {
                if (vehicleModel == null)
                {
                    _logger.LogError("Make object sent from the client is null");
                    return BadRequest("Make object is null");
                }
                if (ModelState.IsValid)
                {
                    _logger.LogError("Make object sent from the client is invalid");
                    return BadRequest("Make object is invalid");
                }

                var makeEntity = _repository.VehicleModel.GetModelById(Id);
                if (makeEntity == null)
                {
                    _logger.LogError($"Make with  Id: {Id}, was not found in the database");
                    return NotFound();
                }
                _mapper.Map(vehicleModel, makeEntity);
                _repository.VehicleModel.UpdateVehicleModel(makeEntity);
                _repository.save();

                return View(vehicleModel);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with the UpdateVehicleMake action: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }

        // GET: VehicleModels/Delete/5
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var vehicleModel = _repository.VehicleModel
                .GetModelById(Id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleModel =  _repository.VehicleModel.GetModelById(id);
            _repository.VehicleModel.Delete(vehicleModel);
             _repository.save();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
