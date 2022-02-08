using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Interface;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Repository
{
    public class MakeRepository : RepositoryBase<VehicleMake>, IMakeRepository
    {
        public MakeRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }

        public IEnumerable<VehicleMake> GetAllMakes()
        {
            return FindAll()
                .OrderBy(mk => mk.Name)
                .ToList();
        }

        public VehicleMake GetMakeById(int Id)
        {
            return FindByCondition(VehicleMake => VehicleMake.Id.Equals(Id))
                .FirstOrDefault();
        }

        public VehicleMake GetMakeWithDetails(int Id)
        {
            return FindByCondition(VehicleMake => VehicleMake.Id.Equals(Id))
                .Include(mo => mo.VehicleModels)
                .FirstOrDefault();
        }       
    }
}
