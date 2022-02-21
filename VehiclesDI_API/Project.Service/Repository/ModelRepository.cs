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
        public ModelRepository(ApplicationDbContext applicationDbContext, ISortHelper<VehicleModel> sortHelper )
            : base(applicationDbContext)
        {

        }
        /*
        public PaginatedList<VehicleModel> GetAllModels(ModelParams modelParams)
        {
            

            //   var sortedModels = _sortHelper.ApplySort(models, modelParams.OrderBy);
           // return PaginatedList<VehicleModel>.ToPaginatedList(FindAll().OrderBy(mo => mo.Name),
            //                               modelParams.pageNumber, modelParams.pageSize);
        } */

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
