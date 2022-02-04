using Project.MVC.Data;
using Project.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Project.Service.VehicleService
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IVehicleMakeService _iVehicleMakeService;
        public VehicleMakeService(ApplicationDbContext context, IVehicleMakeService iVehicleMakeService)
        {
            _context = context;
            _iVehicleMakeService = iVehicleMakeService;
        }
        public IEnumerable<VehicleMake> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<VehicleMake> GetVehicleWithPagination(string sortOrder)

        {

            var vehicleMakes = from s in _context.VehicleMake
                               select s;
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
            return vehicleMakes;
        }

        
    }
}
