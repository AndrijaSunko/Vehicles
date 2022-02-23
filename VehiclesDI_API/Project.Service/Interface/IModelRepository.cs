using Project.Service.Helpers;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interface
{
    public interface IModelRepository : IRepository<VehicleModel>
    {
        IEnumerable<VehicleModel> GetAllModels(string sortOrder,
                                                string currentFilter,
                                                string searchString
                                                );
        // PaginatedList<VehicleModel> GetAllModels(ModelParams modelParams);
        VehicleModel GetModelById(int Id);
        VehicleModel GetModelsWithDetails(int Id);

    }
}
