using MyMoviesApi.Database;
using MyMoviesApi.Database.Entities;

namespace MovieApp
{
    public class MovieService : IMoviesService
    {
        private readonly MoviesDb moviesDb;

        public MovieService(MoviesDb moviesDb)
        {
            this.moviesDb = moviesDb;
        }

        public IEnumerable<Movie> GetMovies()
        {
            var movies = moviesDb.Movies.ToList();
            return movies.Select(MapToDTO);
        }

        public Movie GetMovie(int id)
        {
            var movie = moviesDb.Movies.FirstOrDefault(m => m.id == id) ?? throw new Exception("Movie not found");
            return MapToDTO(movie);
        }

        public Movie AddMovie(Movie movie)
        {
            if (moviesDb.Movies.Any(m => m.title == movie.title))
            {
                throw new Exception("A movie with the same title already exists");
            }
            var movieEntity = MovieEntity.Create(movie.title, movie.director, movie.year, movie.rate);
            moviesDb.Movies.Add(movieEntity);
            moviesDb.SaveChanges();
            movie.id = movieEntity.id;
            return MapToDTO(movieEntity);
        }

        public Movie UpdateMovie(Movie movie)
        {
            var movieEntity = moviesDb.Movies.FirstOrDefault(m => m.id == movie.id) ?? throw new Exception("Movie not found");
            movieEntity.title = movie.title;
            movieEntity.director = movie.director;
            movieEntity.year = movie.year;
            movieEntity.rate = movie.rate;

            moviesDb.SaveChanges();

            return MapToDTO(movieEntity);
        }

        public void DeleteMovie(int id)
        {
            var movie = moviesDb.Movies.FirstOrDefault(m => m.id == id) ?? throw new Exception("Movie not found");
            moviesDb.Movies.Remove(movie);
            moviesDb.SaveChanges();
        }


        public Movie MapToDTO(MovieEntity entity)
        {
            return new Movie
            {
                id = entity.id,
                title = entity.title,
                director = entity.director,
                year = entity.year,
                rate = entity.rate
            };
        }
    }

}