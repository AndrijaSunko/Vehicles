using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Helpers;
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

        

        public PagedList<VehicleMake> GetAllMakes(MakeParams makeParams)
        {
            return PagedList<VehicleMake>.ToPagedList(FindAll().OrderBy(mk => mk.Name),
                                           makeParams.pageNumber, makeParams.pageSize);
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

        public void CreateVehicleMake(VehicleMake vehicleMake) => Create(vehicleMake);

        public void UpdateVehicleMake(VehicleMake vehicleMake) => Update(vehicleMake);

        public void DeleteVehicleMake(VehicleMake vehicleMake) => Delete(vehicleMake);

    }
}
