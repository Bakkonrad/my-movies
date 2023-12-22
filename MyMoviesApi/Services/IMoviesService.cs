namespace MovieApp
{
    public interface IMoviesService
    {
        IEnumerable<Movie> GetMovies();
        Movie GetMovie(int id);
        Movie AddMovie(Movie movie);
        Movie UpdateMovie(Movie movie);
        void DeleteMovie(int id);
    }

}

