using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeleBot.Models;
using TeleBot.ViewModels;

namespace TeleBot.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        BotContext bc;
        public OrderController(BotContext context)
        {
            bc = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                var ord = new Order { OrderDesc = model.Description, OrderDate = DateTime.Now, PersonId = bc.Persons.Where(x => x.Email == User.Identity.Name).Select(x => x.PersonId).FirstOrDefault(), Status = "Ожидание" };
                bc.Order.Add(ord);
                await bc.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
