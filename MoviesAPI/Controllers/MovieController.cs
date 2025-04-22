using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private static List<Movie> movies = new List<Movie>();

        [HttpPost]
        public void AddMovie([FromBody] Movie movie)
        {
            movies.Add(movie);
            Console.WriteLine(movie.Title);
        }
    }
}
