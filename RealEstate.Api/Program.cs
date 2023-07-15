using Microsoft.OpenApi.Models;
using RealEstate.Api;
using RealEstate.Api.Middlewares;
using RealEstate.Application;
using RealEstate.DataAccess;
using RealEstate.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;

// Add project dependecies
builder.Services.AddRealEstateApi();
builder.Services.AddRealEstateApplication(config);
builder.Services.AddRealEstateDataAccess(config);
builder.Services.AddRealEstateServices();

// Set Cors
builder.Services.AddCors(cors =>
    cors.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
);

// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "RealEstateAPI", Version = "v2" });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
    // Do migrations and initialize database when start app, if you don't want this approach, comment
    await app.InitializeDatabaseAsync();

app.UseCors("CorsPolicy");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "RealEstateAPI");
});

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
