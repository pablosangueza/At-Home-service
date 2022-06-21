using AtHome.APIHandler.handlers;
using AtHome.APIHandler.interfaces;
using AtHome.DataRepository.interfaces;
using AtHome.DataRepository.repositories;
using AtHome.Service.interfaces;
using AtHome.Service.services;
using AtHome.ShippingCompanyAPI.handlers;
using AtHome.ShippingCompanyAPI.interfaces;
using AtHome.ShippingCompanyAPI.resolvers;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

Console.WriteLine("============> " + builder.Environment.EnvironmentName + " <============");

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
builder.Services.AddTransient<ISCHandlerResolver, SCHandlerResolver>();
builder.Services.AddTransient<ISCApiHandler, RestApiHandler>();
builder.Services.AddTransient<ISCApiHandler, MockRandomApiHandler>();
builder.Services.AddTransient<ISCApiHandler, SoapApiHandler>();
builder.Services.AddTransient<ISCApiHandler, RpcApiHandler>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.UseStaticFiles();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.MapControllers();

app.Run();
