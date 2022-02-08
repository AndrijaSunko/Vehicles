using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.DataTransferObjects
{
    public class MakeForCreationDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage ="Name can't be longer than 20 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(5, ErrorMessage = "Name can't be longer than 5 characters")]
        public string Abrv { get; set; }
    }
}
