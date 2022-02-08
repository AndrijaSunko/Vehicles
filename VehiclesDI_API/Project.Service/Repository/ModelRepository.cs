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
        private ISortHelper<VehicleModel> _modelSortModelHelper;
        public ModelRepository(ApplicationDbContext applicationDbContext, ISortHelper<VehicleModel> modelSortHelper )
            : base(applicationDbContext)
        {

        }

        public PagedList<VehicleModel> GetAllMakes(ModelParams modelParams)
        {
            throw new NotImplementedException();
        }

        public VehicleMake GetMakeById(int Id)
        {
            throw new NotImplementedException();
        }

        public VehicleMake GetMakeWithDetails(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
