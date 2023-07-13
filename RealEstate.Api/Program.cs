using Microsoft.OpenApi.Models;
using RealEstate.Api.Middlewares;
using RealEstate.Application;
using RealEstate.FileService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;

// Add project dependecies
builder.Services.AddAddRealEstateApplication(config);
builder.Services.AddRealEstateFileService();

// Set Cors
builder.Services.AddCors(cors =>
    cors.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
);

// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "RealEstateAPI", Version = "v2" });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("CorsPolicy");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "RealEstateAPI");
});

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
