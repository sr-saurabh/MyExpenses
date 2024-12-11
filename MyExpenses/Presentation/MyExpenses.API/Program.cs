using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MyExpenses.API;
using MyExpenses.Infrastructure.Postgres;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

MyExpenses.API.ServiceCollectionExtensions.configuration = builder.Configuration;

builder.Services.AddAllDependencies();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Configure Bearer token authentication for Swagger
    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        }
    );
    // Apply the Bearer token security globally to all endpoints
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>() // No specific scopes required
            }
        }
    );
    // Map DateOnly type to a specific format in Swagger documentation
    c.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "MM-dd-yyyy"
    });
    // Include XML comments in Swagger (if enabled in the project settings)
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });

    //allowing definite access to a particular origin
    //options.AddPolicy("s", policy =>
    //{
    //    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    //});
});
var app = builder.Build();

// Configure t`he HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();
app.MapGet("/api/weather", () =>
{
    return Results.Ok(new { Temperature = "22°C", Condition = "Sunny" });
});
app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).ToList();
    return Results.Ok(forecast);
});

//adding those cors configuration 
//app.UseCors("s");

app.UseCors(options =>
{
    options.WithOrigins(["ss","sss"])
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseAuthentication();
app.UseAuthorization();

app.Run();
