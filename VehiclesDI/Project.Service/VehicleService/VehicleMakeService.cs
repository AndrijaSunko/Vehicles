using Project.MVC.Data;

using Project.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Service;


namespace Project.Service.VehicleService
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly ApplicationDbContext _context;
       // private readonly IVehicleMakeService _iVehicleMakeService;

        public VehicleMakeService(ApplicationDbContext context /*IVehicleMakeService iVehicleMakeService*/)
        {
            _context = context;
          // _iVehicleMakeService = iVehicleMakeService;
        }

        

        public IEnumerable<VehicleMake> VehicleSort(string sortOrder, string searchString, int? pageNumber, int pageSize = 3)

        {               
            
            var vehicleMakes = from s in _context.VehicleMake
                               select s;

            if (searchString != null)
            {
                pageNumber = 1;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleMakes = vehicleMakes.Where(s => s.Name.Contains(searchString)
                );
            }


            switch (sortOrder)
            {
                case "name_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(s => s.Name);
                    break;
                
                default:
                    vehicleMakes = vehicleMakes.OrderBy(s => s.Name);
                    break;
            }
            
            return vehicleMakes;
        }        
    }
}
