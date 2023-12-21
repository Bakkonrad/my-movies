namespace MovieApp
{
    public interface IMoviesService
    {
        IEnumerable<Movie> GetMovies();
        Movie FindMovie(int id);
        Movie AddMovie(Movie movie);
        Movie UpdateMovie(Movie movie);
        void DeleteMovie(int id);
    }

}

