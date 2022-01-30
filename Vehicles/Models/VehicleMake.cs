using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicles.Models
{
    public class VehicleMake
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        public string Abrv { get; set; }


        [ForeignKey("MakeId")]
        public virtual ICollection<VehicleModel> VehicleModels { get; set; }

        [ForeignKey("MakeId")]
        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
