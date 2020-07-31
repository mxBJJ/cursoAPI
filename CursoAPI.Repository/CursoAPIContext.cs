using CursoAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CursoAPI.Repository
{
    public class CursoAPIContext : DbContext
    {

        public CursoAPIContext(DbContextOptions<CursoAPIContext> options) : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestranteEventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<RedeSocial> RedeSociais { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>().HasKey(PE => new { PE.EventoId, PE.PalestranteId });
        }
    }
}