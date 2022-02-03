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
        Task<VehicleModel[]> GetIncompleteItemsAsync(VehicleModel[] items);
    }
}
