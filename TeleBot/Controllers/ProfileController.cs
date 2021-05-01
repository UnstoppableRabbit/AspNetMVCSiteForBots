using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeleBot.Models;

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
    }
}
