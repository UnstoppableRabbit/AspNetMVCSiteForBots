using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeleBot.Models;
using TeleBot.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;
using System.Security.Cryptography;

namespace TeleBot.Controllers
{
    public class AccountController : Controller
    {
        private BotContext db;
        MD5 md = MD5.Create();
        public AccountController(BotContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Person user = await db.Persons.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == GetMD5(md, model.Password));
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Person Person = await db.Persons.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (Person == null)
                {
                    var user = new Person { Email = model.Email, Password = GetMD5(md, model.Password), FirstName = model.FirstName, LastName = model.LastName, Status = "u" };
                    // добавляем пользователя в бд
                    db.Persons.Add(user) ;
                    await db.SaveChangesAsync();

                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(Person Ps)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, Ps.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, Ps.Status)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        static string GetMD5(MD5 md5, string pass)
        {
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(pass));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
