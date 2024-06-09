using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

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