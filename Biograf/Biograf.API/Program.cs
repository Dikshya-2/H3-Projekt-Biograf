using Biograf.API.Authorization;
using Biograf.API.Help;
using Biograf.Repo.Interface;
using Biograf.Repo.Interface.GenericInterface;
using Biograf.Repo.Models;
using Biograf.Repo.Models.Entities;
using Biograf.Repo.Repositories;
using Biograf.Repo.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string conStr = @"Server=LAPTOP-8GRDQ8JJ; Database=Biograf; Trusted_Connection=true; TrustServerCertificate=true";
builder.Services.AddDbContext<DatabaseContext>(obj => obj.UseSqlServer(conStr));

builder.Services.AddScoped<IActor,ActorRepo>();
builder.Services.AddScoped<IAuthor,AuthorRepo>();
builder.Services.AddScoped<IMovie, MovieRepo>();
builder.Services.AddScoped<ILanguage, LanguageRepo>();
builder.Services.AddScoped<ICategory, CategoryRepo>();
builder.Services.AddScoped<IPhotoRepo, PhotoRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IGeneric<Movie>, GenericRepo<Movie>>();
builder.Services.AddScoped<IGeneric<Language>, GenericRepo<Language>>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();

//core
builder.Services.AddCors(options =>
{
    options.AddPolicy("Coffee",
                          policy =>
                          {
                              policy.AllowAnyOrigin() // This is really bad outside TEC
                                     .AllowAnyHeader()
                                     .AllowAnyMethod();
                          });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// used when injecting appSettings.Secret into jwtUtils
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Biograg.API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Coffee");

//JWT middleware setup, use this instead of default Authorixtion
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
