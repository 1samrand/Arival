using Arival.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace Arival.Infrastructure.Persistance
{
    public class ArivalDbContext : DbContext
    {
        
        public ArivalDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqlServerOptionsExtension =
                optionsBuilder.Options.FindExtension<NpgsqlOptionsExtension>();
            if (sqlServerOptionsExtension != null)
            {
                string connectionString = sqlServerOptionsExtension.ConnectionString;
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public DbSet<UserOtp> UserOtps { get; set; }
    }
}
