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
            new Pelicula(){Titulo="Spider-Man: Far away home",
                Lanzamiento = new DateTime(2019, 11, 2),
            Poster="https://m.media-amazon.com/images/M/MV5BNDMyYTc5ZjYtZTUyNC00MDgxLWE1YTctNGUzOTkxNGE4NjAzXkEyXkFqcGdeQXVyODc0OTEyNDU@._V1_UX100_CR0,0,100,100_AL_.jpg"},
            new Pelicula(){Titulo="Moana",
                Lanzamiento = new DateTime(2001,10,15),
            Poster="https://m.media-amazon.com/images/M/MV5BMzQzMzgzMDIyM15BMl5BanBnXkFtZTgwNTYzMDE2MDI@._V1_UY100_CR25,0,100,100_AL_.jpg"},
            new Pelicula(){Titulo="Inception",
                Lanzamiento = new DateTime(2006, 12, 20),
            Poster="https://m.media-amazon.com/images/M/MV5BMTQyMDMwMDg0Ml5BMl5BanBnXkFtZTcwODM3ODI3Nw@@._V1_UX100_CR0,0,100,100_AL_.jpg"}
          };
        }
    }
}
