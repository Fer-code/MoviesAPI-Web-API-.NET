using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Dtos;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class MovieController : ControllerBase
    {
        //Queremos que o controlador acesse o context
        private MovieContext _context;

        private IMapper _mapper;

        public MovieController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddMovie([FromBody] CreateMovieDto movieDto)
        {
            Movie movie = _mapper.Map<Movie>(movieDto);
            _context.Movies.Add(movie);

            //Salvar alteracoes
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id},
                movie);
        }

        [HttpGet]
        public IEnumerable<Movie> GetAllMovies([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _context.Movies.Skip(skip).Take(take);
        }

        /**
         * Movie? -> indicates that this method can return null, if doesnt exist a movie if the specified id
         */
        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            
            //If movie null apear 404 not found
            if(movie == null) return NotFound();
            return Ok(movie);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDto movieDto) 
        { 
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if(movie == null) return NotFound();

            _mapper.Map(movieDto, movie);
            _context.SaveChanges();

            //quando faz alteração em um recurso usar noContent
            return NoContent();
        }


        /**
         * Update patch - just update specified fields
         */
        [HttpPatch("{id}")]
        public IActionResult UpdateMoviePatch(int id, JsonPatchDocument<UpdateMovieDto> patch)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (movie == null) return NotFound();

            //Converter para DTO o filme que acabamos de recuperar do BD
            var movieToUpdate = _mapper.Map<UpdateMovieDto>(movie);

            //verificar se é valido
            patch.ApplyTo(movieToUpdate, ModelState);

            //se nao for valido
            if(!TryValidateModel(movieToUpdate))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(movieToUpdate, movie);
            _context.SaveChanges();

            //quando faz alteração em um recurso usar noContent
            return NoContent();
        }
    }
}
