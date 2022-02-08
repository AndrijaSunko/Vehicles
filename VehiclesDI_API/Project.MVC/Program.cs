using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Microsoft.Extensions.DependencyInjection;
using Project.Service.Models;
using Project.Service;
using Project.Service.Repository;
using Project.Service.Service;
using Microsoft.AspNetCore.Builder;
using NLog;
using Project.Service.Interface;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


builder.Services.AddHttpClient();
builder.Services.AddControllers();

builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

// builder.Services.AddTransient<IRepository<VehicleMake>, RepositoryVehicleMake>();
// builder.Services.AddTransient<VehicleMakeService, VehicleMakeService>();

 // builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
 //  app.UseSwagger();
   
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// swagger default UI
/*
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseSwagger(x => x.SerializeAsV2 = true);
*/
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
