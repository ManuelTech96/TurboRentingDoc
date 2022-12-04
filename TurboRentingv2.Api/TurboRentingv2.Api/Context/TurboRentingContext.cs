using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TurboRentingv2.Api.Models.Entities;
using TurboRentingv2.Api.Models.EntitiyTypes;

namespace TurboRentingv2.Api.Context
{
    public class TurboRentingContext : DbContext
    {
        
        private readonly IConfiguration configuration;
        public DbSet<User>? Users{ get; set; }
        public DbSet<Vehicule>? Vehicules { get; set; }
        public DbSet<Client>? Clients { get; set; }
        public DbSet<Contract>? Contracts { get; set; }
        public DbSet<Garage>? Garages { get; set; }

        public TurboRentingContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbConnection = configuration.GetConnectionString("DB1");
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(dbConnection);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            callCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }


        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void callCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityTypeConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VehiculeEntityTypeConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GarageEntityTypeConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContractEntityTypeConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientEntityTypeConfiguration).Assembly);
        }

    }
}
