using Api.EF;
using Api.Repositories.DiceRepository;
using Api.Repositories.ProfileRepository;
using Api.Repositories.ThrowRepository;
using Api.Services.ThrowService;
using ApiGRPCDiceyProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

//Dépendances pour BaseRepository
builder.Services.AddDbContext<ApiDbContextStubbed>();
builder.Services.AddScoped<ApiDbContext, ApiDbContextStubbed>();

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<IDiceRepository, SimpleDiceRepository>();
builder.Services.AddScoped<IProfileRepository, SimpleProfileRepository>();
builder.Services.AddScoped<IThrowRepository, SimpleThrowRepository>();
builder.Services.AddScoped<IThrowService, SimpleThrowService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GRPCServiceThrow>();
//app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

