namespace MovieDapperProject.Models
{
    public class MoviesModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int ReleaseYear { get; set; }
        public int Duration { get; set; }

        public decimal Budget { get; set; }
        public decimal Revenue { get; set; }

        // FK'ler
        public int GenreId { get; set; }
        public int DirectorId { get; set; }

        // JOIN için (Dapper mapping)
        public string GenreName { get; set; }
        public string DirectorName { get; set; }


    }
}
