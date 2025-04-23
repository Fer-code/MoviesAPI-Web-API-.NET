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

        //
        private static int id = 0;


        [HttpPost]
        public void AddMovie([FromBody] Movie movie)
        {
            movie.Id = id++;
            movies.Add(movie);
            Console.WriteLine(movie.Title);
        }

        [HttpGet]
        public IEnumerable<Movie> GetAllMovies([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return movies.Skip(skip).Take(take);
        }

        /**
         * ? -> indicates that this method can return null, if doesnt exist a movie if the specified id
         */
        [HttpGet("{id}")]
        public Movie? GetMovieById(int id)
        {
            return movies.FirstOrDefault(movie => movie.Id == id);
        }
    }
}
