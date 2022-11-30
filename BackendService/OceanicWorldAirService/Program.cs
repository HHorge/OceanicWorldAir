using OceanicWorldAirService.Helpers;
using OceanicWorldAirService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRouteFindingService, RouteFindingService>();

RegisterDbContext(builder);

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

static void RegisterDbContext(WebApplicationBuilder builder)
{
    // Database ConnectionString Options
    builder.Services.Configure<ConnectionStringOptions>(builder.Configuration.GetSection(ConnectionStringOptions.ConnectionStrings));
    var connectionStringOptions = new ConnectionStringOptions();
    builder.Configuration.GetSection(ConnectionStringOptions.ConnectionStrings).Bind(connectionStringOptions);

    // To be implemented - Db connection
    //
    //var connectionString = connectionStringOptions.ServiceDatabase;
    //var serverVersion = ServerVersion.AutoDetect(connectionString);
    //builder.Services.AddDbContext<FinanceServiceContext>(
    //    dbContextOptions => dbContextOptions
    //        .UseMySql(connectionString, serverVersion)
    //        .LogTo(Console.WriteLine, LogLevel.Information) // The following three options help with debugging
    //        .EnableSensitiveDataLogging()
    //        .EnableDetailedErrors());
}
