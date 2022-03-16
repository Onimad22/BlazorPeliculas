using BlazorPeliculas.Server.Helpers;
using BlazorPeliculas.Shared.DTOs;
using BlazorPeliculas.Shared.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPeliculas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "peliculas";

        public PeliculasController(ApplicationDbContext context, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PeliculaVisualizarDTO>> Get(int id)
        {
            var pelicula = await context.Peliculas.Where(x => x.Id == id)
                .Include(x => x.GeneroPeliculas)
                .ThenInclude(x => x.Genero)
                .Include(x => x.PeliculaActores)
                .ThenInclude(x => x.Persona)
                .FirstOrDefaultAsync();

            if (pelicula == null) { return NotFound(); }

            //todo:sistema de votacion
            var promedioVotos = 4;
            var votoUsuario = 5;

            pelicula.PeliculaActores = pelicula.PeliculaActores.OrderBy(x => x.Orden).ToList();
            var model = new PeliculaVisualizarDTO();
            model.Pelicula = pelicula;
            model.Generos = pelicula.GeneroPeliculas.Select(x => x.Genero).ToList();
            model.Actores = pelicula.PeliculaActores.Select(x =>
            new Persona()
            {
                Nombre = x.Persona.Nombre,
                Foto = x.Persona.Foto,
                Personaje = x.Personaje,
                Id = x.Persona.Id,
            }).ToList();

            model.PromedioVotos = promedioVotos;
            model.VotoUsuario= votoUsuario;
            return model;
        }

        [HttpGet]
        public async Task<ActionResult<HomePageDTO>> Get()
        {
            var limite = 6;
            var peliculasEnCartelera = await context.Peliculas
                .Where(x => x.EnCartelera)
                .Take(limite)
                .OrderByDescending(x => x.Lanzamiento)
                .ToListAsync();

            var fechaActual = DateTime.Today;

            var proximosEstrenos = await context.Peliculas
                .Where(x => x.Lanzamiento > fechaActual)
                .OrderBy(x => x.Lanzamiento)
                .Take(limite)
                .ToListAsync();

            var response = new HomePageDTO()
            {
                PeliculasEnCartelera = peliculasEnCartelera,
                ProximosEstrenos = proximosEstrenos
            };

            return response;

        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Pelicula pelicula)
        {
            if (!string.IsNullOrEmpty(pelicula.Poster))
            {
                var fotoPoster = Convert.FromBase64String(pelicula.Poster);
                pelicula.Poster = await almacenadorArchivos.GuardarArchivo(fotoPoster, "jpg", contenedor);
            }

            if (pelicula.PeliculaActores != null)
            {
                for (int i = 0; i < pelicula.PeliculaActores.Count; i++)
                {
                    pelicula.PeliculaActores[i].Orden = i + 1;
                }
            }

            

            context.Add(pelicula);
            await context.SaveChangesAsync();
            return pelicula.Id;
        }
    }
}
