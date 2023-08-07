using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WatchProjectAPI.Data;
using WatchProjectAPI.Model;
using WatchProjectAPI.Repositories;
using WatchProjectAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

//inject dbcontext
services.AddDbContext<WatchDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
//inject repository
services.AddScoped<IWatchRepository, WatchRepository>();
//inject services
services.AddScoped<IWatchService, WatchService>();
//inject filesettings/appsettings
services.Configure<FileSettings>(builder.Configuration.GetSection("FileSettings"));


services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


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
