using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.DataTransferObjects
{
    public class MakeForUpdateDto
    {
      //  [Required(ErrorMessage = "Name is required")]
      //  [StringLength(20, ErrorMessage = "Name can't be longer than 20 characters")]
        public string Name { get; set; }

     //   [Required(ErrorMessage = "Name is required")]
     //   [StringLength(5, ErrorMessage = "Name can't be longer than 5 characters")]
        public string Abrv { get; set; }

    }
}
