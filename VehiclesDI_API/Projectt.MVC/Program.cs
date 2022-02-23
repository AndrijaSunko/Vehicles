using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Project.MVC2;
using Project.MVC2.Data;
using Project.Service.Data;
using Project.Service.Helpers;
using Project.Service.Interface;
using Project.Service.Models;
using Project.Service.Repository;
using Project.Service.Service;
using ReflectionIT.Mvc.Paging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPaging(options => {
    options.ViewName = "Bootstrap4";
    options.PageParameterName = "pageindex";
});


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
 builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)); 



builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterType<MakeRepository>().As<IMakeRepository>().InstancePerLifetimeScope();
    builder.RegisterType<ModelRepository>().As<IModelRepository>().InstancePerLifetimeScope();
    builder.RegisterType<SortHelper<VehicleMake>>().As<ISortHelper<VehicleMake>>().InstancePerLifetimeScope();
    builder.RegisterType<SortHelper<Project.Service.Models.VehicleModel>>().As<ISortHelper<Project.Service.Models.VehicleModel>>().InstancePerLifetimeScope();
    builder.RegisterType<LoggerManager>().As<ILoggerManager>().SingleInstance();
    builder.RegisterType<RepositoryWrapper>().As<IRepositoryWrapper>().InstancePerLifetimeScope();
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
