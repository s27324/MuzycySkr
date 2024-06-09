using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;
using WebApplication1.Repository;
using WebApplication1.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IMuzykaService, MuzykaService>();
builder.Services.AddScoped<IMuzykaRepository, MuzykaRepository>();

builder.Services.AddDbContext<MuzykaDbContext>(opt =>
{
    string connString = builder.Configuration.GetConnectionString("Default");
    opt.UseSqlServer(connString);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();