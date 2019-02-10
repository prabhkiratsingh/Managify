using Managify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Managify.Controllers
{
    [Authorize]
    public class RegisteredEventController : Controller
    {
        // Database.
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: RegisteredEvent
        public ActionResult Index()
        {
            // Displays the registered events by that User.
            var userId = User.Identity.GetUserId();
            var userAccount = db.UserAccounts.Where(c => c.ApplicationUserId == userId).First();
            return View(userAccount.RegisteredEvents.ToList());
        }

        public ActionResult Remove(int eventId, int userId)
        {
            // Removes a registered event by the user.
            try
            {
                RegisteredEvent registeredEvent = db.RegisteredEvents.Where
                    (
                        i => (i.UserAccountId == userId && i.EventId == eventId)
                    ).Single();
                db.RegisteredEvents.Remove(registeredEvent);
                db.SaveChanges();
                return RedirectToAction("Index", "RegisteredEvent");
            }
            catch
            {
                return RedirectToAction("Index", "RegisteredEvent");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult List()
        {
            return View(db.RegisteredEvents.ToList());
        }
    }
}