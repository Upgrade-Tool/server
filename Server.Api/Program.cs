using Server.Api.Data;
using Microsoft.EntityFrameworkCore;
using Server.Api.Models;
using Microsoft.AspNetCore.Identity;
using Server.Api.Hubs;
using Server.Api.Repositories;
using Server.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    }); //So we can use enums text, not just numbers
builder.Services.AddSignalR();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<BrandService>();
builder.Services.AddScoped<ICarGroupRepository, CarGroupRepository>();
builder.Services.AddScoped<CarGroupService>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<IOfficeRepository, OfficeRepository>();
builder.Services.AddScoped<OfficeService>();
builder.Services.AddScoped<IKioskDisplayRepository, KioskDisplayRepository>();
builder.Services.AddScoped<KioskDisplayService>();
builder.Services.AddScoped<IKioskStateRepository, KioskStateRepository>();




builder.Services.AddDbContext<AppDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 4;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddRoles<IdentityRole<Guid>>()  // enables AspNetRoles + AspNetUserRoles
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
            .AllowCredentials()
            .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<KioskHub>("/hubs/kiosk");

app.Run();
