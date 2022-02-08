﻿using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interface
{
    public interface IMakeRepository :IRepository<VehicleMake>
    {
        IEnumerable<VehicleMake> GetAllMakes();  
        VehicleMake GetMakeById(int Id);
        VehicleMake GetMakeWithDetails (int Id);
    }
}
