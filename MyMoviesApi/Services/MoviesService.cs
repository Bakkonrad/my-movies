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
            throw new NotImplementedException();
            // return moviesDb.Movies;
        }

        public Movie FindMovie(int id)
        {
            throw new NotImplementedException();
            // return moviesDb.Movies.Find(id);
        }

        public Movie AddMovie(Movie movie)
        {
            // moviesDb.Movies.Add(movie);
            moviesDb.SaveChanges();
            return movie;
        }

        public Movie UpdateMovie(Movie movie)
        {
            // moviesDb.Movies.Update(movie);
            moviesDb.SaveChanges();
            return movie;
        }

        public void DeleteMovie(int id)
        {
            var movie = moviesDb.Movies.Find(id);
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