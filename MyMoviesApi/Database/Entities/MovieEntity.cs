namespace MyMoviesApi.Database.Entities
{
    public class MovieEntity
    {
        public int id { get; set; }
        public string title { get; set; }
        public string director { get; set; }
        public int year { get; set; }
        public int rate { get; set; }


        public static MovieEntity Create(string title, string director, int year, int rate)
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
