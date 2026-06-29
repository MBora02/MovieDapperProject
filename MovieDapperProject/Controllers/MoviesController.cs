using Microsoft.AspNetCore.Mvc;
using Dapper;
using MovieDapperProject.Models;

namespace MovieDapperProject.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View(Context.Listeleme<MoviesModel>("sp_MovieViewAll"));
        }

        public IActionResult EY(int id = 0)
        {
            // dropdownlar için
            ViewBag.Genres = Context.Listeleme<GenresModel>("sp_GenreViewAll");
            ViewBag.Directors = Context.Listeleme<DirectorsModel>("sp_DirectorViewAll");

            if (id == 0)
            {
                return View();
            }
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@MovieId", id);

                return View(
                    Context.Listeleme<MoviesModel>("sp_MovieViewById", param)
                    .FirstOrDefault()
                );
            }
        }

        [HttpPost]
        public IActionResult EY(MoviesModel movie)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@MovieId", movie.Id);
            param.Add("@Title", movie.Title);
            param.Add("@Description", movie.Description);
            param.Add("@ReleaseYear", movie.ReleaseYear);
            param.Add("@Duration", movie.Duration);
            param.Add("@Budget", movie.Budget);
            param.Add("@Revenue", movie.Revenue);
            param.Add("@GenreId", movie.GenreId);
            param.Add("@DirectorId", movie.DirectorId);

            Context.ExecuteReturn("sp_MovieEY", param);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id = 0)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@MovieId", id);

            Context.ExecuteReturn("sp_MovieSil", param);

            return RedirectToAction("Index");
        }
    }
}