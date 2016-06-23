using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutomatedSchedulingSystem.Models;

namespace AutomatedSchedulingSystem.Controllers
{
    public class RequestsController : Controller
    {
        private SlateDBContext db = new SlateDBContext();

        // GET: Requests
        public ActionResult Index()
        {

            ViewBag.Message = "Currently no approved requests.";

            //var requests = from request in db.request
            //             select request;

            //requests = requests.Where(r => r.EmployeeID.Equals(User.Identity.GetUserId()));

            List<Requests> requests = new List<Requests>();

            foreach (var item in db.request)
            {
                if (item.EmployeeID.UserID.Equals(User.Identity.GetUserId()))
                {
                    requests.Add(item);
                }
            }

            if (requests.Count() == 0)
            {
                return View(requests.ToList());
            }

            else
            {
                return View(requests);
            }
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requests requests = db.request.Find(id);
            if (requests == null)
            {
                return HttpNotFound();
            }
            return View(requests);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Requests requests)
        {

            List<Employee> currentEmployee;

            string currentUser = System.Web.HttpContext.Current.User.Identity.Name;

            currentEmployee = (db.employee.Where(x => x.Email == currentUser).ToList());

            //List<Day> dayOfRequest;            

            //dayOfRequest = (db.day.Where(x => x.DayOfWeek == requests.DayID.DayOfWeek).ToList());

            if (ModelState.IsValid)
            {
                requests.EmployeeID = currentEmployee[0];
                requests.Status = "Pending";                
                List<Day> dayOfRequest = (db.day.Where(x => x.DayOfWeek == requests.DayID.DayOfWeek).ToList()); ;
                requests.DayID = dayOfRequest[0];
                db.request.Add(requests);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requests);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requests requests = db.request.Find(id);
            if (requests == null)
            {
                return HttpNotFound();
            }
            return View(requests);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Reason,Status")] Requests requests)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requests).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requests);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requests requests = db.request.Find(id);
            if (requests == null)
            {
                return HttpNotFound();
            }
            return View(requests);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Requests requests = db.request.Find(id);
            db.request.Remove(requests);
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
