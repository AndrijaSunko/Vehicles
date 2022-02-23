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
       
    }
}
