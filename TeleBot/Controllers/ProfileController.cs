using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TeleBot.Models;
using TeleBot.ViewModels;

namespace TeleBot.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private BotContext bc;

        public ProfileController(BotContext context)
        {
            bc = context;
        }
        
        public IActionResult Index()
        {
            Person user = (from p in bc.Persons
                           where p.Email == HttpContext.User.Identity.Name
                           select p).FirstOrDefault();
            return View(user);
        }
        public IActionResult Change()
        {
            ChangeModel q = new ChangeModel
            {
                user = (from p in bc.Persons
                        where p.Email == HttpContext.User.Identity.Name
                        select p).FirstOrDefault()
            };
            return View(q);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Change(ChangeModel model)
        {
            Person person = new Person();
            if (ModelState.IsValid)
            {
                person = await bc.Persons.FirstOrDefaultAsync(u => u.Email == HttpContext.User.Identity.Name);
                if (person != null)
                {
                    person.FirstName = model.FirstName;
                    person.LastName = model.LastName;
                    await bc.SaveChangesAsync();
                }
                else
                    return RedirectToAction("Register", "Account");
            }
            return RedirectToAction("Index", "Profile");
        }
    }
}
