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
    public class RepositoryWrapper : IRepositoryWrapper

    {
        private ApplicationDbContext _DbContext;
        private IMakeRepository _make;
        private IModelRepository _model;
        private ISortHelper<VehicleMake> _makeSortHelper;
        private ISortHelper<VehicleModel> _modelSortHelper;

        public IMakeRepository VehicleMake
        {
            get
            {
                if(_make == null)
                {
                    _make = new MakeRepository(_DbContext, _makeSortHelper);
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
                    _model = new ModelRepository(_DbContext, _modelSortHelper);
                }
                return _model;
            }
        }

        public RepositoryWrapper(ApplicationDbContext applicationDbContext,
            ISortHelper<VehicleMake> makeSortHelper,
            ISortHelper<VehicleModel> modelSortHelper)

        {
            _DbContext = applicationDbContext;
            _makeSortHelper = makeSortHelper;
            _modelSortHelper = modelSortHelper;
        }
        public void save()
        {
            _DbContext.SaveChanges();
        }
    }
}
