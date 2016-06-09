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
    public class ScheduleController : Controller
    {
        private SlateDBContext db = new SlateDBContext();

        // GET: Schedule
        public ActionResult Index()
        {

            List<Shift> userShifts = new List<Shift>();

            List<Employee> allEmployees = new List<Employee>();

            string currentUserID = User.Identity.GetUserId();

            //allEmployees = (from employee in db.employee
            //                where employee.UserID.Equals(currentUserID)
            //                select (employee)).ToList<Employee>();

            //userShifts = (from shift in db.shift
            //              where shift.EmployeeID.ID.Equals(allEmployees[0].ID)
            //              select (shift)).ToList<Shift>();            

            userShifts.AddRange(db.shift.Where(x => true).ToList());

            return View(db.shift.Where(x => x.EmployeeID.UserID == currentUserID).ToList());          

        }

        // GET: Schedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shift shift = db.shift.Find(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            return View(shift);
        }

        // GET: Schedule/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StartTime,EndTime,Type,Status")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                db.shift.Add(shift);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shift);
        }

        // GET: Schedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shift shift = db.shift.Find(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            return View(shift);
        }

        // POST: Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StartTime,EndTime,Type,Status")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shift).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shift);
        }

        // GET: Schedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shift shift = db.shift.Find(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            return View(shift);
        }

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shift shift = db.shift.Find(id);
            db.shift.Remove(shift);
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
