using OceanicWorldAirService.Helpers;
using OceanicWorldAirService.Services;
using Microsoft.EntityFrameworkCore;
using OceanicWorldAirService.Context;
using OceanicWorldAirService.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "OceanicAir",
        Version = "v1"
    });
});
builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyHeader())
);

// Register Services 
builder.Services.AddScoped<IRouteFindingService, RouteFindingService>();
builder.Services.AddScoped<IShippingHttpRequester, ShippingHttpRequester>();
builder.Services.AddScoped<ICostCalculationService, CostCalculationService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

RegisterFinanceDbContext(builder);

var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void RegisterFinanceDbContext(WebApplicationBuilder builder)
{
    // Finance Db ConnectionString Options
    builder.Services.Configure<ConnectionStringOptions>(builder.Configuration.GetSection(ConnectionStringOptions.ConnectionStrings));
    var connectionStringOptions = new ConnectionStringOptions();
    builder.Configuration.GetSection(ConnectionStringOptions.ConnectionStrings).Bind(connectionStringOptions);

    var connectionString = connectionStringOptions.ServiceDatabase;
    builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
}
