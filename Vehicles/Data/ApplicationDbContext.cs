using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicles.Models;

namespace Vehicles.Data
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(250)]
        public string FirstName { get; set; }
        [StringLength(250)]
        public string LastName { get; set; }
        [StringLength(250)]
        public string Adress { get; set; }
        [StringLength(50)]
        public string PostCode { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<AppUser> Users { get; set; }

    }
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Vehicles.Models.VehicleMake> VehicleMake { get; set; }
        public DbSet<Vehicles.Models.VehicleModel> VehicleModel { get; set; }
    }
}