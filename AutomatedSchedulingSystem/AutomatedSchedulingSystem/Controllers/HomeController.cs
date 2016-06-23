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

            List<Employee> currentEmployee;

            string currentUser = System.Web.HttpContext.Current.User.Identity.Name;

            currentEmployee = (db.employee.Where(x => x.Email == currentUser).ToList());

            if (currentEmployee.Count == 0)
            {
                return View();
            }
            else if (currentEmployee[0].Role == "Associate")
            {
                return View("AssociateIndex");
            }
            else
            {
                return View("ManagerIndex");
            }          

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