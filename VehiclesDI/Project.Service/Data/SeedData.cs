using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.MVC.Data;
using Project.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.VehicleMake.Any())
                {
                    return;   // DB has been seeded
                }

                context.VehicleMake.AddRange(

                    new VehicleMake
                    {
                        Name = "BMW",
                        Abrv = "BMW"
                    },

                    new VehicleMake
                    {
                        Name = "Volkswagen",
                        Abrv = "VW"
                    },

                    new VehicleMake
                    {
                        Name = "Ford",
                        Abrv = "Ford"
                    },
                    new VehicleMake
                    {
                        Name = "Citroen",
                        Abrv = "Cit"
                    },
                    new VehicleMake
                    {
                        Name = "Rolls Royce",
                        Abrv = "RR"
                    }




                );
                context.SaveChanges();

                context.VehicleModel.AddRange(

                    new VehicleModel
                    {
                        MakeId = 1,
                        Name = "BMW",
                        Abrv = "x5",
                    },
                    new VehicleModel
                    {
                        MakeId = 2,
                        Name = "Volkswagen",
                        Abrv = "Golf",
                    },
                    new VehicleModel
                    {
                        MakeId = 3,
                        Name = "Ford",
                        Abrv = "Focus",
                    },
                    new VehicleModel
                    {
                        MakeId = 4,
                        Name = "Citroen",
                        Abrv = "C5",
                    },
                    new VehicleModel
                    {
                        MakeId = 5,
                        Name = "Rolls Royce",
                        Abrv = "PE",
                    }


                    );
                context.SaveChanges();
            }
        }
    }
}
