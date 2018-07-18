using Microsoft.EntityFrameworkCore;
using Airport.DAL.Entities;


namespace Airport.DAL
{
    public class AirportContext : DbContext
    {
        public DbSet<Aeroplane> Aeroplanes { get; set; }

        public DbSet<AeroplaneType> AeroplaneTypes { get; set; }

        public DbSet<Crew> Crews { get; set; }

        public DbSet<Departure> Departures { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Pilot> Pilots { get; set; }

        public DbSet<Stewardess> Stewardesses { get; set; }

        public DbSet<Ticket> Tickets { get; set; }


        public AirportContext(DbContextOptions<AirportContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.Migrate();
            AirportInitializer.IntializateIfEmpty(this);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne(x => x.Flight)
                .WithMany(x => x.Tickets);

            modelBuilder.Entity<Flight>()
                .HasMany(x => x.Tickets)
                .WithOne(x => x.Flight);

            modelBuilder.Entity<Crew>()
                .HasOne(x => x.Pilot)
                .WithMany(x => x.Crews);

            modelBuilder.Entity<Pilot>()
                .HasMany(x => x.Crews)
                .WithOne(x => x.Pilot);

            modelBuilder.Entity<Crew>()
                .HasOne(x => x.Pilot)
                .WithMany(x => x.Crews);

            modelBuilder.Entity<Crew>()
                .HasOne(x => x.Pilot)
                .WithMany(x => x.Crews);

            modelBuilder.Entity<Crew>()
                 .HasMany(x => x.Departures)
                 .WithOne(x => x.Crew);

            modelBuilder.Entity<AeroplaneType>()
                .HasMany(x => x.Aeroplanes)
                .WithOne(x => x.AeroplaneType);

            modelBuilder.Entity<Departure>()
                 .HasOne(x => x.Airplane)
                 .WithMany(x => x.Departures);
        }
    }
}
