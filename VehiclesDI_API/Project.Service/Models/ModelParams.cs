using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class ModelParams : QueryStringParams
    {
        public ModelParams()
        {
            OrderBy = "Name";
        }
    }
}
