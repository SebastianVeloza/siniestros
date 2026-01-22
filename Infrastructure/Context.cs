using Domain.Entities;
using Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class Context : DbContext
    {
        public DbSet<historico_refresh_token> historico_refresh_token { get; set; }
        public DbSet<Logs_Siniestros> Logs_Siniestros { get; set; }
        public DbSet<Siniestros> Siniestros { get; set; }
        public DbSet<tipos_siniestro> tipos_siniestro { get; set; }
        public DbSet<ciudades> ciudades { get; set; }
        public DbSet<departamentos> departamentos { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuración de las relaciones y restricciones
            #region historico_refresh_token
            modelBuilder.Entity<historico_refresh_token>().HasKey(e => e.historico_refresh_token_id);
            #endregion

            #region Logs_Siniestros
            modelBuilder.Entity<Logs_Siniestros>().HasKey(e => e.Logs_Siniestros_id);
            #endregion
            
            #region departamentos
            modelBuilder.Entity<departamentos>().HasKey(e => e.departamentos_id);
            #endregion

            #region ciudades
            modelBuilder.Entity<ciudades>().HasKey(e => e.ciudades_id);
            modelBuilder.Entity<ciudades>()
             .HasOne(x => x.Departamentos)
             .WithMany()
             .HasForeignKey(x => x.departamentos_id);
            #endregion

            #region tipos_siniestro
            modelBuilder.Entity<tipos_siniestro>().HasKey(e => e.tipos_siniestro_id);
            #endregion

            #region Siniestros
            modelBuilder.Entity<Siniestros>()
               .HasKey(x => x.Siniestros_id);

            modelBuilder.Entity<Siniestros>()
             .HasOne(x => x.Tipos_Siniestro)
             .WithMany()
             .HasForeignKey(x => x.tipos_siniestro_id);

            modelBuilder.Entity<Siniestros>()
             .HasOne(x => x.Ciudades)
             .WithMany()
             .HasForeignKey(x => x.ciudades_id);

            modelBuilder.Entity<Siniestros>()
             .HasOne(x => x.Departamentos)
             .WithMany()
             .HasForeignKey(x => x.departamentos_id);
            #endregion

        }

    }
}

