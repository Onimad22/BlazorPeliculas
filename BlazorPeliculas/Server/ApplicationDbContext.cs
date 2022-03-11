using BlazorPeliculas.Shared.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculas.Server
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeneroPelicula>().HasKey(e => new { e.PeliculaId,e.GeneroId });
            modelBuilder.Entity<PeliculaActor>().HasKey(e => new { e.PeliculaId, e.PersonaId });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<GeneroPelicula> GenerosPeliculas { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<PeliculaActor> PeliculasActores { get; set; }
    }
}
