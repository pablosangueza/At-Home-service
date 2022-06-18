using AtHome.APIHandler.handlers;
using AtHome.APIHandler.interfaces;
using AtHome.DataRepository.interfaces;
using AtHome.DataRepository.repositories;
using AtHome.Service.interfaces;
using AtHome.Service.services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "At Home API",
                    Description = "At Home API by Pablo Luis Sangueza"
                    
                });
            }
);

// Dependency Injection
builder.Services.AddTransient<IShippingService, ShippingService>();
builder.Services.AddTransient<IRepository, DataRepository>();
builder.Services.AddTransient<IShippingCompanyAPI, ShippingCompanyAPI>();



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
