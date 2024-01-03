namespace MyMoviesApi.Database.Entities
{
    public class MovieEntity
    {
        public int id { get; set; }
        public string title { get; set; }
        public string director { get; set; }
        public int year { get; set; }
        public double rate { get; set; }


        public static MovieEntity Create(string title, string director, int year, double rate)
        {
            var entity = new MovieEntity
            {
                title = title,
                director = director,
                year = year,
                rate = rate
            };
            
            return entity;
        }
    }
    
}
