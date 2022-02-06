using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Service.Entities;

namespace Project.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Project.Service.Entities.VehicleMake> VehicleMake { get; set; }
        public DbSet<Project.Service.Entities.VehicleModel> VehicleModel { get; set; }
    }
}