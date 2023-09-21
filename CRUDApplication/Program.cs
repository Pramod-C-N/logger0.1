using CRUDApplication.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net.AspNetCore;

[assembly:log4net.Config.XmlConfigurator(Watch = true)]
var builder = WebApplication.CreateBuilder(args);


// Enable internal debugging for log4net 
log4net.Util.LogLog.InternalDebugging = true;


//clear inbuilt logger providers
builder.Logging.ClearProviders();

////log4net Registration added
//builder.Logging.AddLog4Net();


// initialize log4net
log4net.Config.XmlConfigurator.Configure(new FileInfo("log4nets.config"));



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
app.Logger.LogInformation("xxxxxx");

app.MapControllers();

app.Run();
