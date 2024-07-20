using System.Text.Json.Serialization;
using AirlineReservation;
using AirlineReservation.Data;
using AirlineReservation.Repository;
using AirlineReservation.Repository.IRepository;
using AirlineReservation.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AirlineContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AirlineSQLConnection"));
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Configure JSON options to use string representation for enums
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAutoMapper(typeof(MappingConfig));

// Register Repository
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

// Register Services
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IReservationService, ReservationService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
