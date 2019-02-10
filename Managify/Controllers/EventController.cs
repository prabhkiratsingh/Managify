using Managify.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace Managify.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Event
        public ActionResult Index()
        {
            // Displays the List of events available in a card format.
            return View(db.Events.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult List()
        {
            // Here the Admin is able to edit as well as delete an event.
            return View(db.Events.ToList());
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Event eventModel)
        {
            // Creation of event.
            try
            {
                if (ModelState.IsValid)
                {
                    // Fetching the filename.
                    string FileName = Path.GetFileNameWithoutExtension(eventModel.ImageFile.FileName);

                    //To Get File Extension  
                    string FileExtension = Path.GetExtension(eventModel.ImageFile.FileName);

                    //Get Upload path from Web.Config file AppSettings.  
                    string UploadPath = HttpContext.Server.MapPath("~/UploadedImages/");
                    eventModel.ImagePath = UploadPath + eventModel.Title.Trim() + FileExtension;

                    //To copy and save file into server.  
                    eventModel.ImageFile.SaveAs(eventModel.ImagePath);
                    eventModel.ImagePath = "/UploadedImages/" + eventModel.Title.Trim() + FileExtension;
                    db.Events.Add(eventModel); // Adding the newly created event to the DbSet.
                    db.SaveChanges();
                    return RedirectToAction("Create", "Event");
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Event/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            // Editing the events.
            var eventModel = db.Events.Find(id);
            return View(eventModel);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Event eventModel)
        {
            try
            {
                // Same as that in create mode.
                if (ModelState.IsValid)
                {
                    string FileName = Path.GetFileNameWithoutExtension(eventModel.ImageFile.FileName);

                    //To Get File Extension  
                    string FileExtension = Path.GetExtension(eventModel.ImageFile.FileName);

                    //Get Upload path from Web.Config file AppSettings.  
                    string UploadPath = HttpContext.Server.MapPath("~/UploadedImages/");
                    eventModel.ImagePath = UploadPath + eventModel.Title.Trim() + FileExtension;

                    //To copy and save file into server.  
                    eventModel.ImageFile.SaveAs(eventModel.ImagePath);
                    eventModel.ImagePath = "/UploadedImages/" + eventModel.Title.Trim() + FileExtension;
                    db.Entry(eventModel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("List");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Event/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            // Delete an event.
            try
            {
                Event eventModel = db.Events.Where( i => (i.Id == id) ).Single();
                db.Events.Remove(eventModel);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Event/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Register(int id)
        {
            // Action called by a user to Register to an event.
            try
            {
                var userId = User.Identity.GetUserId();
                var userAccount = db.UserAccounts.Where(c => c.ApplicationUserId == userId).First();
                // Checking if the user has already registered.
                if(db.RegisteredEvents.Where(i => i.UserAccountId == userAccount.Id && i.EventId == id).Count() == 0)
                {
                    var registeredEvent = new RegisteredEvent()
                    {
                        UserAccountId = userAccount.Id,
                        EventId = id
                    };
                    db.RegisteredEvents.Add(registeredEvent);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Event");
            }
            catch
            {
                return RedirectToAction("Index", "Event");
            }
        }
    }
}
