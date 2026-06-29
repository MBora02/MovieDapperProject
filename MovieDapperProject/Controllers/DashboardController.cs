using Microsoft.AspNetCore.Mvc;
using MovieDapperProject.Models;

namespace MovieDapperProject.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var model = new DashboardModel();
            model.TotalMovies = Context.Listeleme<int>("sp_Dash_TotalMovies").FirstOrDefault();
            model.TotalReviews = Context.Listeleme<int>("sp_Dash_TotalReviews").FirstOrDefault();
            model.TotalGenres = Context.Listeleme<int>("sp_Dash_TotalGenres").FirstOrDefault();

            var bestMovie = Context.Listeleme<dynamic>("sp_Dash_BestProfitMovie").FirstOrDefault();
            model.BestMovieTitle = bestMovie?.Title;
            model.BestMovieProfit = bestMovie?.Profit ?? 0;

            var bestDirector = Context.Listeleme<dynamic>("sp_Dash_BestDirector").FirstOrDefault();
            model.BestDirectorName = bestDirector?.DirectorName;
            model.BestDirectorMovieCount = bestDirector?.MovieCount ?? 0;

            var bestGenre = Context.Listeleme<dynamic>("sp_Dash_BestGenre").FirstOrDefault();
            model.BestGenreName = bestGenre?.Name;
            model.BestGenreMovieCount = bestGenre?.MovieCount ?? 0;

            return View(model);
        }
    }
}
