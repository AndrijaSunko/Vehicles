#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Service.Entities;

namespace Project.MVC.Data
{
    public class ProjectMVCContext : DbContext
    {
        public ProjectMVCContext (DbContextOptions<ProjectMVCContext> options)
            : base(options)
        {
        }

        public DbSet<Project.Service.Entities.VehicleModel> VehicleModel { get; set; }
    }
}
