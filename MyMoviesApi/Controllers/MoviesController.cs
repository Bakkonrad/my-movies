using Microsoft.AspNetCore.Mvc;
using MovieApp;

namespace MyMoviesApi.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> logger;
        private readonly IMoviesService moviesService;

        public MoviesController(ILogger<MoviesController> logger, IMoviesService moviesService)
        {
            this.logger = logger;
            this.moviesService = moviesService;
        }

        [HttpGet]
        public IEnumerable<Movie> GetPeople()
        {
            return moviesService.GetMovies();
        }

        [HttpGet("{id}")]
        public Movie FindMovie([FromRoute] int id)
        {
            return moviesService.FindMovie(id);
        }

        [HttpPost]
        public Movie AddPerson([FromBody] Movie movie)
        {
            return moviesService.AddMovie(movie);
        }
    }
}