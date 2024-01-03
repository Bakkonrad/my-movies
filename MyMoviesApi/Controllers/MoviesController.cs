using Microsoft.AspNetCore.Mvc;
using MovieApp;
using Newtonsoft.Json;

namespace MyMoviesApi.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> logger;
        private readonly IMoviesService moviesService;
        private readonly HttpClient _httpClient;

        public MoviesController(ILogger<MoviesController> logger, IMoviesService moviesService)
        {
            this.logger = logger;
            this.moviesService = moviesService;
            _httpClient = new HttpClient();
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

        //Get movies from external api
        [HttpGet("external")]
        public async Task<IEnumerable<Movie>> SyncExternalMovies()
        {
            var apiUrl = "https://filmy.programdemo.pl/MyMovies";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var externalMovies = JsonConvert.DeserializeObject<List<Movie>>(response);
            return moviesService.SyncExternalMovies(externalMovies);
        }

        [HttpPost]
        public Movie AddMovie([FromBody] Movie movie)
        {
            return moviesService.AddMovie(movie);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie([FromBody] Movie movie)
        {
            try
            {
                movie.id = int.Parse(RouteData.Values["id"].ToString());
                var updatedMovie = moviesService.UpdateMovie(movie);
                return Ok(updatedMovie);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error updating movie with id {movie.id}");
                return BadRequest("Error updating movie");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
                moviesService.DeleteMovie(id);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error deleting movie with id {id}");
                return BadRequest("Error deleting movie");
            }
        }
    }
}