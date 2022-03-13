using BlazorPeliculas.Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorPeliculas.Client.Repositorios
{
    public class Repositorio : IRepositorio
    {
        private readonly HttpClient httpClient;
        public Repositorio(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private JsonSerializerOptions OpcionesPorDefectoJson =>
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
          
            var responseHTTP = await httpClient.GetAsync(url);
            if (responseHTTP.IsSuccessStatusCode)
            {
                var response=await DescerializarRespuesta<T>(responseHTTP,OpcionesPorDefectoJson);
                return new HttpResponseWrapper<T>(response, false, responseHTTP);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, true, responseHTTP);
            }
        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PutAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }


        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response=await DescerializarRespuesta<TResponse>(responseHttp,OpcionesPorDefectoJson);
                return new HttpResponseWrapper<TResponse>(response, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, true, responseHttp);
            }
        }
        private async Task<T> DescerializarRespuesta<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, jsonSerializerOptions);
        }

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
