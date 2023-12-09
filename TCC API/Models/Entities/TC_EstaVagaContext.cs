using Microsoft.EntityFrameworkCore;
using TCC_API.Models.Entities.Parking;
using TCC_API.Models.Entities.User;
using TCC_API.Models.EntitiesConfig.Parking;
using TCC_API.Models.EntitiesConfig.User;

namespace TCC_API.Models.Entities
{
    public partial class TC_EstaVagaContext : DbContext
    {
        public TC_EstaVagaContext()
        {
        }

        public TC_EstaVagaContext(DbContextOptions<TC_EstaVagaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Parking.Parking> Parking { get; set; }
        public virtual DbSet<User.User> User { get; set; }
        public virtual DbSet<UserSession> UserSession { get; set; }
        public virtual DbSet<Vacancy> Vacancy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ParkingConfig());

            modelBuilder.ApplyConfiguration(new UserConfig());

            modelBuilder.ApplyConfiguration(new UserSessionConfig());

            modelBuilder.ApplyConfiguration(new VacancyConfig());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}