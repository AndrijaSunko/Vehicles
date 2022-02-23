using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Helpers;
using Project.Service.Interface;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Repository
{
    public class ModelRepository : RepositoryBase<VehicleModel>, IModelRepository
    {
        private ISortHelper<VehicleModel> _sortModelHelper;
        private readonly ApplicationDbContext _context;
        public ModelRepository(ApplicationDbContext applicationDbContext, ISortHelper<VehicleModel> sortHelper )
            : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public IQueryable<VehicleModel> GetAllModels(string sortOrder,
                                                string currentFilter,
                                                string searchString
                                                )
        {
            var models = from s in _context.VehicleModel
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                models = models.Where(s => s.Name.Contains(searchString)
                                       || s.Abrv.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    models = models.OrderByDescending(s => s.Name);
                    break;

                case "abrv_desc":
                    models = models.OrderByDescending(s => s.Name);
                    break;
                case "MakeId_desc":
                    models = models.OrderByDescending(s => s.MakeId);
                    break;
                default:
                    models = models.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            return models;
        }
       

            public VehicleModel GetModelById(int Id)
        {
            return FindByCondition(VehicleModel => VehicleModel.Id.Equals(Id))
                 .FirstOrDefault();
        }

        public VehicleModel GetModelsWithDetails(int Id)
        {
            return FindByCondition(VehicleModel => VehicleModel.Id.Equals(Id))
                  .FirstOrDefault();
        }
    }
}
