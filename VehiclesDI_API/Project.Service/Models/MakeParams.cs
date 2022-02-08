using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class MakeParams : QueryStringParams
    {
        public MakeParams()
        {
            OrderBy = "Name";
        }
        public int MinId { get; set; }

        public int MaxId { get; set; }

        public bool ValidId => MaxId > MinId;
    }
}
