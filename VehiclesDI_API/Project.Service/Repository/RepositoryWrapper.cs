using Project.Service.Data;
using Project.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper

    {
        private ApplicationDbContext _DbContext;
        private IMakeRepository _make;
        private IModelRepository _model;

        public IMakeRepository VehicleMake
        {
            get
            {
                if(_make == null)
                {
                    _make = new MakeRepository(_DbContext);
                }
                return _make;
            }
        }

        public IModelRepository VehicleModel
        {
            get
            {
                if(_model == null)
                {
                    _model = new ModelRepository(_DbContext);
                }
                return _model;
            }
        }

        public RepositoryWrapper(ApplicationDbContext applicationDbContext)
        {
            _DbContext = applicationDbContext;
        }
        public void save()
        {
            _DbContext.SaveChanges();
        }
    }
}
