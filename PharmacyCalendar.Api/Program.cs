using Microsoft.OpenApi.Models;
using PharmacyCalendar.Api.Configuration;
using PharmacyCalendar.Application.Features.Query;
using PharmacyCalendar.Infrastructure.Configuration;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddPersistance(builder.Configuration);


#region [- MediatR -]

var assembly = Assembly.GetExecutingAssembly();
var appAssembly = typeof(GetTechnicalOfficerByIdQuery).Assembly;
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly, appAssembly));

#endregion

#region [- Swagger() -]

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PharmacyCalendar API",
        Version = "v1",
        Description = "API for managing technical officers"
    });
});

#endregion

var app = builder.Build();
app.InitializeDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "PharmacyCalendar.API v1");
    });
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }
