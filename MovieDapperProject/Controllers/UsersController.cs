using Dapper;
using Microsoft.AspNetCore.Mvc;
using MovieDapperProject.Models;

namespace MovieDapperProject.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UsersModel user)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserName", user.UserName);
            param.Add("@Email", user.Email);
            param.Add("@PasswordHash", user.PasswordHash);

            Context.ExecuteReturn("sp_UserRegister", param);

            return RedirectToAction("Login");
        }

        // ---------------- LOGIN ----------------
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UsersModel user)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserName", user.UserName);
            param.Add("@PasswordHash", user.PasswordHash);

            var result = Context.Listeleme<UsersModel>("sp_UserLogin", param)
                                .FirstOrDefault();

            if (result != null)
            {
                HttpContext.Session.SetString("UserName", result.UserName);
                HttpContext.Session.SetInt32("UserId", result.Id);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid login!";
            return View();
        }

        // ---------------- LOGOUT ----------------
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // ---------------- CHANGE PASSWORD ----------------
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string oldPassword, string newPassword)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;

            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", userId);
            param.Add("@OldPasswordHash", oldPassword);
            param.Add("@NewPasswordHash", newPassword);

            Context.ExecuteReturn("sp_UserChangePassword", param);

            return RedirectToAction("Index", "Home");
        }
    }
}
