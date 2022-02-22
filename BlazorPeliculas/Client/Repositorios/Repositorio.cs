using BlazorPeliculas.Shared.Entidades;
using System;
using System.Collections.Generic;

namespace BlazorPeliculas.Client.Repositorios
{
    public class Repositorio : IRepositorio
    {
        public List<Pelicula> ObtenerPeliculas()
        {
            return new List<Pelicula>()
        {
            new Pelicula(){Titulo="Spider-Man: Far away home",Lanzamiento = new DateTime(2019, 11, 2)},
            new Pelicula(){Titulo="Moana",Lanzamiento = new DateTime(2001,10,15)},
            new Pelicula(){Titulo="Inception",Lanzamiento = new DateTime(2006, 12, 20)}
          };
        }
    }
}
