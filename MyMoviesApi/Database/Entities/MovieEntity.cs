namespace MyMoviesApi.Database.Entities
{
    public class MovieEntity
    {
        public int id { get; set; }
        public string title { get; set; }
        public string director { get; set; }
        public int year { get; set; }
        public int rate { get; set; }


        public MovieEntity Create(int id, string title, string director, int year, int rate)
        {
            this.id = id;
            this.title = title;
            this.director = director;
            this.year = year;
            this.rate = rate;
            return this;
        }
    }
}
