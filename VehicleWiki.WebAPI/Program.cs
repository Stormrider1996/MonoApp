using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using VehicleWiki.DAL;
using VehicleWiki.Repository;
using VehicleWiki.Repository.Common;
using VehicleWiki.Service;
using VehicleWiki.Service.Common;
using VehicleWiki.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Use Autofac as the ServiceProviderFactory
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register services directly with Autofac here
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    builder.RegisterType<VehicleMakeRepository>().As<IVehicleMakeRepository>().InstancePerLifetimeScope();
    builder.RegisterType<VehicleModelRepository>().As<IVehicleModelRepository>().InstancePerLifetimeScope();
    builder.RegisterType<VehicleMakeService>().As<IVehicleMakeService>().InstancePerLifetimeScope();
    builder.RegisterType<VehicleModelService>().As<IVehicleModelService>().InstancePerLifetimeScope();
    builder.RegisterAutoMapper(typeof(AutoMapperProfile).Assembly);
});

// Add services to the container.
builder.Services.AddDbContext<VehicleDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = false;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
