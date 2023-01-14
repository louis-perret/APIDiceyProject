using Api.EF;
using Api.Repositories.DiceRepository;
using Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dépendances en cascade pour AbstractDiceController.
builder.Services.AddTransient<IDiceService, SimpleDiceService>();
builder.Services.AddTransient<IDiceRepository, SimpleDiceRepository>();

//Dépendances pour BaseRepository
builder.Services.AddDbContext<ApiDbContextStubbed>();
builder.Services.AddScoped<ApiDbContext, ApiDbContextStubbed>();

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
