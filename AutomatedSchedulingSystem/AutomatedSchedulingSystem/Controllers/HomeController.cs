using AutomatedSchedulingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace AutomatedSchedulingSystem.Controllers
{
    public class HomeController : Controller
    {

        SlateDBContext db = new SlateDBContext();

        public ActionResult Index()
        {

            //bool loggedIn = User.Identity.IsAuthenticated;            

            //if (loggedIn)
            //{
                
            //    return View(loggedIn);
            //}
            //else
            //{
                return View();
            //}
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

        public ActionResult AddRequest()
        {

            ViewBag.Message = "Currently no approved requests.";

            var requests = from request in db.request
                           select request;

            requests = requests.Where(r => r.EmployeeID.Equals(User.Identity.GetUserId()));

            if (requests.Count() == 0)
            {
                return View(ViewBag.Message);
            }

            else
            {
                return View(requests);
            }
        }

    }
}