using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interface
{
    public interface IRepositoryWrapper
    {
        IMakeRepository VehicleMake { get; }
        IModelRepository VehicleModel { get; }
        void save();
    }
}
