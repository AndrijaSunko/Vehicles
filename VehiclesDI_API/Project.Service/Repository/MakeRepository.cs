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
        
        public IEnumerable<VehicleMake> GetAllMakes(string sortOrder,
                                                string currentFilter,
                                                string searchString
                                                )
        {
            var makes = from s in _context.VehicleMake
                        select s;
          
            
                searchString = currentFilter;
            
           
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
            return makes.ToList();
           // return PaginatedList<VehicleMake>.ToPaginatedList(FindAll().OrderBy(mk => mk.Name), pageNumber ?? 1, pageSize);
        }

        /*
        public PagedList<VehicleMake> GetAllMakes(MakeParams makeParams)
        {
            var makes = FindByCondition(o => o.Id >= makeParams.MinId &&
                                o.Id <= makeParams.MaxId)
                            .OrderBy(on => on.Name);

         //   var sortedMakes = _sortHelper.ApplySort(makes, makeParams.OrderBy);
            return PagedList<VehicleMake>.ToPagedList(FindAll().OrderBy(mk => mk.Name),
                                           makeParams.pageNumber, makeParams.pageSize);
        } */

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
