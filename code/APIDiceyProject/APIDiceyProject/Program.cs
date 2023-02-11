using Api.EF;
using Api.Repositories.DiceRepository;
using Api.Repositories.ProfileRepository;
using Api.Services.DiceFolder;
using Api.Services.ProfileFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dépendances en cascade pour AbstractDiceController.
builder.Services.AddTransient<IDiceService, SimpleDiceService>();
builder.Services.AddTransient<IDiceRepository, SimpleDiceRepository>();

//Dépendances en cascade pour AbstractProfileController
builder.Services.AddTransient<IProfileService, SimpleProfileService>();
builder.Services.AddTransient<IProfileRepository, SimpleProfileRepository>();

//Dépendances pour BaseRepository
builder.Services.AddDbContext<ApiDbContextStubbed>();
builder.Services.AddScoped<ApiDbContext, ApiDbContextStubbed>();



//Versionnage de l'API
builder.Services.AddApiVersioning(v => v.ApiVersionReader = new UrlSegmentApiVersionReader());

builder.Services.AddVersionedApiExplorer(o =>
{
    o.GroupNameFormat = "'v'VVV";
    o.SubstituteApiVersionInUrl = true;
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
});


builder.Services.AddSwaggerGen(
options =>
{
    var provider = builder.Services.BuildServiceProvider()
     .GetRequiredService<IApiVersionDescriptionProvider>(); foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(
         description.GroupName,
         new OpenApiInfo() 
            {
                Title = $"APIDiceyProject {description.ApiVersion}",
                Version = description.ApiVersion.ToString()
            });
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    //context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
