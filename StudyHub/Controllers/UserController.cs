using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudyHub.Models;

namespace StudyHub
{
    public class UserController : Controller
    {
        private StudyHubDataEntities db = new StudyHubDataEntities();

        // GET: /User/
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Country).Include(u => u.State).Include(u => u.Town);
            return View(users.ToList());
        }

        // GET: /User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName");
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName");
            ViewBag.TownId = new SelectList(db.Towns, "TownId", "TownName");
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UserId,FirstName,LastName,Email,UserPassword,CreatedAt,UpdatedAt,UserType,SeoName,TutorCenterName,PhysicalAddress,CountryId,StateId,TownId,ProfileImageUrl,Degree,GpsCoordinates,description,Phone,AvailablePersonel")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", user.CountryId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", user.StateId);
            ViewBag.TownId = new SelectList(db.Towns, "TownId", "TownName", user.TownId);
            return View(user);
        }

        // GET: /User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", user.CountryId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", user.StateId);
            ViewBag.TownId = new SelectList(db.Towns, "TownId", "TownName", user.TownId);
            return View(user);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UserId,FirstName,LastName,Email,Passwor,CreatedAt,UpdatedAt,UserType,SeoName,TutorCenterName,PhysicalAddress,CountryId,StateId,TownId,ProfileImageUrl,Degree,GpsCoordinates,description,Phone,AvailablePersonel")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", user.CountryId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", user.StateId);
            ViewBag.TownId = new SelectList(db.Towns, "TownId", "TownName", user.TownId);
            return View(user);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
