using Project.Service.Data;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Repository
{
    public class RepositoryVehicleMake : IRepository<VehicleMake>
    {
        ApplicationDbContext _dbContext;

        public RepositoryVehicleMake(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<VehicleMake> Create(VehicleMake _object)
        {
            var obj = await _dbContext.VehicleMake.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Delete(VehicleMake _object)
        {
           _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

        public IEnumerable<VehicleMake> GetAll()
        {
            try
            {
                return _dbContext.VehicleMake.Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public VehicleMake GetById(int Id)
        {
            return _dbContext.VehicleMake.Where(x => x.IsDeleted == false && x.Id == Id).FirstOrDefault();
        }

        public void Update(VehicleMake _object)
        {
            _dbContext.VehicleMake.Update(_object);
            _dbContext.SaveChanges();
        }
    }
}
