using StudyHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyHub.Controllers
{
    public class TutoringCenterController : Controller
    {
        private StudyHubDataEntities db = new StudyHubDataEntities();

        // GET: /TutoringCenter/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult addNewTutoringCenter()
        {
            IDictionary<string, string> dictOpt = new Dictionary<string, string>();
            var coord = Request.Form["gps_coordinates"].Split(',');

            if (coord.Length == 2)
            {
                //hash the password before creating object
                var hashedPassword = "test";
                var user = new User
                {
                    TutorCenterName = Request.Form["tc_name"],
                    Email = Request.Form["email"],
                    UserPassword = hashedPassword,
                    Description = Request.Form["Description"],
                    AvailablePersonel = Convert.ToInt32(Request.Form["AvailablePersonel"]),
                    PhysicalAddress = Request.Form["PhysicalAddress"],
                    GpsCoordinates = Request.Form["GpsCoordinates"],
                    CountryId = Convert.ToInt32(Request.Form["CountryId"]),
                    StateId = Convert.ToInt32(Request.Form["StateId"]),
                    TownId = Convert.ToInt32(Request.Form["TownId"]),
                    Phone = Request.Form["Phone"]
                };
                db.Users.Add(user);
                db.SaveChanges();
                var userId = user.UserId;
                
                if(userId > 0){
                    
                }
                
            }
            else 
            {
                dictOpt["response"] = "invalid cordinates";
            }

            return Json(dictOpt);
        }




        //
        // GET: /TutoringCenter/
        public ActionResult Register()
        {
            ViewBag.countries = db.Countries.SqlQuery("SELECT * FROM [Country]");
            ViewBag.specialities = db.Specialities.SqlQuery("SELECT * FROM [Speciality]");
            return View();
        }
	}
}