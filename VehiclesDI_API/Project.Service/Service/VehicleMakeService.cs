using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Service
{
    public class VehicleMakeService

    {
        private readonly IRepository<VehicleMake> _vehicleMake;

        public VehicleMakeService(IRepository<VehicleMake> vehicleMake)
        {
            _vehicleMake = vehicleMake;
        }

        //get Make details by ID

        public IEnumerable<VehicleMake> GetVehicleMakesById(string Id)
        {
            return _vehicleMake.GetAll().Where(x => x.Name == Id).ToList();
        }

        // get details
        public IEnumerable<VehicleMake> GetVehicleMakes()
        {
            try
            {
                return _vehicleMake.GetAll().ToList(); ;

            }
            catch (Exception)
            {
                throw;
            }
        }

        //get make by name
        public VehicleMake GetVehicleByName(string Name)
        {
            return _vehicleMake.GetAll().Where(x => x.Name == Name).FirstOrDefault();
        }
        //add Make
        public async Task <VehicleMake> AddVehicleMake(VehicleMake vehicleMake)
        {
            return await _vehicleMake.Create(vehicleMake);
        }

        // delete make
        public bool DeleteVehicleMake (string Name)
        {
            try
            {
                var DataList = _vehicleMake.GetAll().Where(x => x.Name == Name).ToList();
                foreach (var item in DataList)
                {
                    _vehicleMake.Delete(item);
                }
                return true;
            }   
            catch (Exception)
            {
                return true;
            }
        }

        //update make
        public bool UpdateVehicleMake(VehicleMake vehicleMake)
        {
            try
            {
                var DataList = _vehicleMake.GetAll().Where(x => x.IsDeleted !=true).ToList();
                foreach (var item in DataList)
                {
                    _vehicleMake.Update(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
