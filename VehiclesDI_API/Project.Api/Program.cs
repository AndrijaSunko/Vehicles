using Project.Service.Data;
using Project.Service.Interface;
using Project.Service.Repository;
using Project.Service.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using NLog;
using Swashbuckle.AspNetCore.Swagger;
using Project.Service.Helpers;
using Project.Service.Models;
using Autofac.Extensions.DependencyInjection;
using Autofac;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Autofac container

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{

builder.RegisterType<SortHelper<VehicleMake>>().As<ISortHelper<VehicleMake>>().InstancePerLifetimeScope();
builder.RegisterType<SortHelper<VehicleModel>>().As<ISortHelper<VehicleModel>>().InstancePerLifetimeScope();
builder.RegisterType<LoggerManager>().As<ILoggerManager>().SingleInstance();
builder.RegisterType<RepositoryWrapper>().As<IRepositoryWrapper>().InstancePerLifetimeScope();
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(Program));

// asp.net core DI
/*
builder.Services.AddScoped<ISortHelper<VehicleMake>, SortHelper<VehicleMake>>();
builder.Services.AddScoped<ISortHelper<VehicleModel>, SortHelper<VehicleModel>>();
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   // app.UseSwagger();
   // app.UseSwaggerUI();

    
}


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
