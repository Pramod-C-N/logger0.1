using CRUDApplication.DAL;
using log4net.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net.AspNetCore;


var builder = WebApplication.CreateBuilder(args);


// Enable internal debugging for log4net 
log4net.Util.LogLog.InternalDebugging = true;


//clear inbuilt logger providers
builder.Logging.ClearProviders();

////log4net Registration added
//builder.Logging.AddLog4Net();


// initialize log4net
XmlConfigurator.Configure(new FileInfo("log4nets.config"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Register Db connection string 
builder.Services.AddDbContext<MyAppDbContext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.Logger.LogInformation("hellooo");

app.MapControllers();

app.Run();
