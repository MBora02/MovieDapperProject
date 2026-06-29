namespace MovieDapperProject.Models
{
    public class DashboardModel
    {
        public int TotalMovies { get; set; }
        public int TotalReviews { get; set; }
        public int TotalGenres { get; set; }

        public string BestMovieTitle { get; set; }
        public decimal BestMovieProfit { get; set; }

        public string BestDirectorName { get; set; }
        public int BestDirectorMovieCount { get; set; }

        public string BestGenreName { get; set; }
        public int BestGenreMovieCount { get; set; }
    }
}
