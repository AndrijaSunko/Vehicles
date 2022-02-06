using Project.MVC.Data;

using Project.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Service;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Project.Service.VehicleService
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IVehicleMakeService _iVehicleMakeService;
        private string? currentFilter;

        public VehicleMakeService(ApplicationDbContext context /*IVehicleMakeService iVehicleMakeService*/)
        {
            _context = context;
          // _iVehicleMakeService = iVehicleMakeService;
        }
   
        

        public IQueryable<VehicleMake> VehicleSort(string sortOrder, string searchString, string currentFilter, int? pageNumber, int pageSize = 3)

        {               
            
            var vehicleMakes = from s in _context.VehicleMake
                               select s;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleMakes = vehicleMakes.Where(s => s.Name.Contains(searchString)
                || s.Abrv.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "name_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(s => s.Name);
                    break;
                case "Abrv_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(s => s.Abrv);
                    break;
                default:
                    vehicleMakes = vehicleMakes.OrderBy(s => s.Name);
                    break;
            }

            return (IQueryable<VehicleMake>)vehicleMakes;
        }        
    }
}
