using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Helpers;
using Project.Service.Interface;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Project.Service.Repository
{
    public class MakeRepository : RepositoryBase<VehicleMake>, IMakeRepository
    {
       private  ISortHelper<VehicleMake> _sortHelper;
        private ApplicationDbContext _context;
        public MakeRepository(ApplicationDbContext applicationDbContext, ISortHelper<VehicleMake> sortHelper)
            : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }
        
        public IQueryable<VehicleMake> GetAllMakes(string sortOrder,
                                                string currentFilter,
                                                string searchString
                                                )
        {
            var makes = from s in _context.VehicleMake
                        select s;
          
                       
            if (!String.IsNullOrEmpty(searchString))
            {
                makes = makes.Where(s => s.Name.Contains(searchString)
                                       || s.Abrv.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    makes = makes.OrderByDescending(s => s.Name);
                    break;
               
                case "abrv_desc":
                    makes = makes.OrderByDescending(s => s.Name);
                    break;
                default:
                    makes = makes.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
          return makes;
            //return PaginatedList<VehicleMake>.ToPaginatedList(FindAll(), pageNumber  , pageSize);
        }

    

        public VehicleMake GetMakeById(int Id)
        {
            return FindByCondition(VehicleMake => VehicleMake.Id.Equals(Id))
                .FirstOrDefault();
        }

        public VehicleMake GetMakeWithDetails(int Id)
        {
            return FindByCondition(VehicleMake => VehicleMake.Id.Equals(Id))
                .Include(mo => mo.VehicleModel)
                .FirstOrDefault();
        }

        public void CreateVehicleMake(VehicleMake vehicleMake) => Create(vehicleMake);

        public void UpdateVehicleMake(VehicleMake vehicleMake) => Update(vehicleMake);

        public void DeleteVehicleMake(VehicleMake vehicleMake) => Delete(vehicleMake);

        }
}
