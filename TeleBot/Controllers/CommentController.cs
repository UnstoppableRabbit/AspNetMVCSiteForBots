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
    public class CommentController : Controller
    {
        BotContext bc;
        public CommentController(BotContext context)
        {
            bc = context;
        }
        public IActionResult Index(int id = 1) {
            CommentModel cm = new CommentModel
            {
                allComs = (from x in bc.Comment
                           join z in bc.Persons on x.PersonId equals z.PersonId
                           select new comModel { Name = $"{z.FirstName} {z.LastName}", 
                                                 Email = z.Email,
                                                 CommentDate = x.CommentDate,
                                                 Text = x.Text,
                                                 CommentId = x.CommentId })
                           .Skip((id - 1) * 5)
                           .Take(5).ToList(),
                CurrentPage = id,
                MaxPage = (int)Math.Ceiling(bc.Comment.Select(x => x).Count() / 5.0)
            };
            if (cm.CurrentPage > cm.MaxPage)
                return RedirectToAction("Index", new { id = 1 });
            return View(cm);
        }
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CommentModel model)
        {
            if (ModelState.IsValid)
            {
                var com = new Comment { Text = model.Comment, CommentDate = DateTime.Now, PersonId = bc.Persons.Where(x => x.Email == User.Identity.Name).Select(x => x.PersonId).FirstOrDefault() };
                bc.Comment.Add(com);
                await bc.SaveChangesAsync();
                return RedirectToAction("Index", "Comment", new { id = 1 });
            }
            else
                return RedirectToAction("Index", "Comment", new { id = 1 });
        }
    }
}
