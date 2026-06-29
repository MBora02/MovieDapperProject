using Microsoft.AspNetCore.Mvc;
using Dapper;
using MovieDapperProject.Models;

namespace MovieDapperProject.Controllers
{
    public class ReviewsController : Controller
    {
        public IActionResult Index()
        {
            var data = Context.Listeleme<Reviews>("sp_ReviewViewAll");
            return View(data);
        }

        public IActionResult ByMovie(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@MovieId", id);

            return View(
                Context.Listeleme<Reviews>("sp_ReviewViewByMovieId", param)
            );
        }

        public IActionResult EY(int id = 0, int movieId = 0)
        {
            ViewBag.Movies = Context.Listeleme<MoviesModel>("sp_MovieViewAll");
            ViewBag.MovieId = movieId;

            if (id == 0)
                return View();

            DynamicParameters param = new DynamicParameters();
            param.Add("@ReviewId", id);

            return View(
                Context.Listeleme<Reviews>("sp_ReviewById", param).FirstOrDefault()
            );
        }

        [HttpPost]
        public IActionResult EY(Reviews review)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@ReviewId", review.Id);
            param.Add("@MovieId", review.MovieId);
            param.Add("@UserName", review.UserName);
            param.Add("@Rating", review.Rating);
            param.Add("@Comment", review.Comment);

            Context.ExecuteReturn("sp_ReviewEY", param);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id = 0)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@ReviewId", id);

            Context.ExecuteReturn("sp_ReviewSil", param);

            return RedirectToAction("Index");
        }
    }
}