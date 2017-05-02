using StudyHub.Models;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace StudyHub.Controllers
{
    public class HomeController : Controller
    {
        private StudyHubDataEntities db = new StudyHubDataEntities();
        
        public ActionResult Index()
        {
            var cnt = 1;
            var location = "";
            var users = db.Users.SqlQuery("SELECT * FROM User WHERE UserType = 'Tutoring Center' OR UserType = 'Tutoring'");
            
            foreach (var user in users)
            {
                var userId = new SqlParameter("@userId", user.UserId);
                var usersSpecialitiesQry = "SELECT s.SpecialityName FROM Speciality AS s JOIN UserSpeciality AS us ON s.SpecialityId = us.SpecialityId WHERE us.UserId = @userId LIMIT 2";
                var userSpecialities = db.Database.SqlQuery<Speciality>(usersSpecialitiesQry,userId);
                
                var coord = user.GpsCoordinates.Split(",".ToCharArray());
                if (user.UserType == "Tutoring Center")
                {
                    var seoUrl = "tutoring-center/" + user.UserId + "/" + user.SeoName;
                    var host = Request.Url.ToString();
                    seoUrl = host + "/" + seoUrl;
                    seoUrl = "<a href='" + seoUrl + "'>" + user.TutorCenterName + "</a>";

                    if (cnt == 1) 
                    {
                        location += "['" + seoUrl + "'," + coord[0] + "," + coord[1] + "," + cnt + "]";
                    }
                    else
                    {
                        location += ",['" + seoUrl + "'," + coord[0] + "," + coord[1] + "," + cnt + "]";
                    }
                }
                else 
                {
                    var seoUrl = "tutor/" + user.UserId + "/" + user.SeoName;
                    var host = Request.Url.ToString();
                    seoUrl = host + "/" + seoUrl;
                    seoUrl = "<a href='" + seoUrl + "'>" + user.FirstName +" " + user.LastName+ "</a>";

                    if (cnt == 1)
                    {
                        location += "['" + seoUrl + "'," + coord[0] + "," + coord[1] + "," + cnt + "]";
                    }
                    else
                    {
                        location += ",['" + seoUrl + "'," + coord[0] + "," + coord[1] + "," + cnt + "]";
                    }
                }

                cnt++;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}