using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeleBot.Models;

namespace TeleBot.Controllers
{
    [Authorize(Roles = "a")]
    public class AdminController : Controller
    {
        private BotContext bc;
        public AdminController(BotContext context)
        {
            bc = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Comments()
        {
            var coms = (from x in bc.Comment
                        select x).ToList();
            return View(coms);
        }

        public IActionResult Orders()
        {
            var ords = (from x in bc.Order
                        select x).ToList();
            return View(ords);
        }
    }
}
