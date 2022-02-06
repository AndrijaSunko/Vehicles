using Project.MVC.Data;
using Project.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.VehicleService
{
    public class VehicleModelService : IVehicleModelService

    {
        private readonly ApplicationDbContext _context;
        public VehicleModelService(ApplicationDbContext context )
        {
            _context = context;        
        }
        public IEnumerable<VehicleModel> VehicleModelSort(string sortOrder, string searchString, int? pageNumber, int pageSize)
        {

            var vehicleModels = from s in _context.VehicleModel
                               select s;

            if (searchString != null)
            {
                pageNumber = 1;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModels = vehicleModels.Where(s => s.Name.Contains(searchString)
                );
            }


            switch (sortOrder)
            {
                case "name_desc":
                    vehicleModels = vehicleModels.OrderByDescending(s => s.Name);
                    break;

                default:
                    vehicleModels = vehicleModels.OrderBy(s => s.Name);
                    break;
            }

            return vehicleModels;
        }
    }
}
