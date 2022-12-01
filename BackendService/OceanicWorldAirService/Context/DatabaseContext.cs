using Microsoft.EntityFrameworkCore;
using OceanicWorldAirService.Models;
using RouteModel = OceanicWorldAirService.Models.Route;

namespace OceanicWorldAirService.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {

        }

        ///// <summary>
        ///// DbSet for <see cref="Booking"/>.
        ///// </summary>
        //public DbSet<Booking> Bookings { get; set; } = null!;

        ///// <summary>
        ///// DbSet for <see cref="Connection"/>.
        ///// </summary>
        //public DbSet<Connection> Connections { get; set; } = null!;

        ///// <summary>
        ///// DbSet for <see cref="Costs"/>.
        ///// </summary>
        //public DbSet<Costs> Costs { get; set; } = null!;

        ///// <summary>
        ///// DbSet for <see cref="Customer"/>.
        ///// </summary>
        //public DbSet<Customer> Customers { get; set; } = null!;

        ///// <summary>
        ///// DbSet for <see cref="Node"/>.
        ///// </summary>
        //public DbSet<Node> Nodes { get; set; } = null!;

        ///// <summary>
        ///// DbSet for <see cref="Parcel"/>.
        ///// </summary>
        //public DbSet<Parcel> Parcels { get; set; } = null!;

        ///// <summary>
        ///// DbSet for <see cref="RouteModel"/>.
        ///// </summary>
        //public DbSet<RouteModel> Routes { get; set; } = null!;
    }
}
