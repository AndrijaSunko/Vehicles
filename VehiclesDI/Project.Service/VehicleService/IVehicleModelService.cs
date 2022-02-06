using Project.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.VehicleService
{
    public interface IVehicleModelService
    {
        IQueryable<VehicleModel> VehicleModelSort(string sortOrder, string searchString, string currentFilter, int? pageNumber, int pageSize);
    }
}
