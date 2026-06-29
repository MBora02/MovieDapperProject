using Dapper;
using Microsoft.AspNetCore.Mvc;
using MovieDapperProject.Models;

namespace MovieDapperProject.Controllers
{
    public class DirectorsController : Controller
    {
        public IActionResult Index()
        {
            return View(Context.Listeleme<DirectorsModel>("sp_DirectorViewAll"));
        }

        public IActionResult EY(int id = 0)
        {
            if (id == 0)
                return View();
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DirectorId", id);

                return View(
                    Context.Listeleme<DirectorsModel>("sp_DirectorViewById", param)
                    .FirstOrDefault()
                );
            }
        }

        [HttpPost]
        public IActionResult EY(DirectorsModel director)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@DirectorId", director.Id);
            param.Add("@FirstName", director.FirstName);
            param.Add("@LastName", director.LastName);
            param.Add("@BirthDate", director.BirthDate);

            Context.ExecuteReturn("sp_DirectorEY", param);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id = 0)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@DirectorId", id);

            Context.ExecuteReturn("sp_DirectorSil", param);

            return RedirectToAction("Index");
        }
    }
}
