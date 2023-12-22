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
        public IEnumerable<Movie> GetMovies()
        {
            return moviesService.GetMovies();
        }

        [HttpGet("{id}")]
        public Movie GetMovie(int id)
        {
            return moviesService.GetMovie(id);
        }

        [HttpPost]
        public Movie AddMovie([FromBody] Movie movie)
        {
            return moviesService.AddMovie(movie);
        }
    }
}