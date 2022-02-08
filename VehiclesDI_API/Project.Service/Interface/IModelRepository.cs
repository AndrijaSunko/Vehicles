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
        PagedList<VehicleModel> GetAllMakes(ModelParams modelParams);
        VehicleMake GetMakeById(int Id);
        VehicleMake GetMakeWithDetails(int Id);

    }
}
