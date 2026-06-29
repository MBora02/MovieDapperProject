using Dapper;
using Microsoft.AspNetCore.Mvc;
using MovieDapperProject.Models;

namespace MovieDapperProject.Controllers
{
    public class GenresController : Controller
    {
        public IActionResult Index()
        {
            return View(Context.Listeleme<GenresModel>("sp_GenreViewAll"));
        }

        public IActionResult EY(int id = 0)
        {
            if (id == 0)
                return View();
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@GenreId", id);

                return View(
                    Context.Listeleme<GenresModel>("sp_GenreViewById", param)
                    .FirstOrDefault()
                );
            }
        }

        [HttpPost]
        public IActionResult EY(GenresModel genre)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@GenreId", genre.Id);
            param.Add("@Name", genre.Name);

            Context.ExecuteReturn("sp_GenreEY", param);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id = 0)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@GenreId", id);

            Context.ExecuteReturn("sp_GenreSil", param);

            return RedirectToAction("Index");
        }
    }
}
