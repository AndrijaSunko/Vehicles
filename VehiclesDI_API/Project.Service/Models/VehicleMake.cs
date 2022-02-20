﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class VehicleMake
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Abrv { get; set; }
        

        [ForeignKey("MakeId")]
        public virtual ICollection<VehicleModel> VehicleModel { get; set; }
        
    }
}
