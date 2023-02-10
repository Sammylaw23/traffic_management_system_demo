using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrafficManagementSystem.API.Extensions;
using TrafficManagementSystem.API.Services;
using TrafficManagementSystem.Application;
using TrafficManagementSystem.Application.Interfaces.Services;
using TrafficManagementSystem.Infrastructure;
using TrafficManagementSystem.Infrastructure.DbContexts;
using TrafficManagementSystem.Infrastructure.Identity;
using TrafficManagementSystem.Infrastructure.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();


//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<TrafficManagementSystemDbContext>(options =>
//    options.UseSqlServer(connectionString));
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors(options => options.AddDefaultPolicy(_builder => _builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.



using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var dbContext = serviceProvider.GetRequiredService<TrafficManagementSystemDbContext>();
        await dbContext.Database.MigrateAsync();
        await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
        await AppIdentityDbContextSeed.SeedOffenceTypes(dbContext);
    }
    catch (Exception ex)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during migration");
    }
}



//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseApiErrorHandler();

app.MapControllers();

app.Run();
