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
    public class ShiftsController : Controller
    {
        private SlateDBContext db = new SlateDBContext();

        List<int> availabilityCountPerWeekDay = new List<int>();

        List<Availability> mondayAvailability;
        List<Availability> tuesdayAvailability;
        List<Availability> wednesdayAvailability;
        List<Availability> thursdayAvailability;
        List<Availability> fridayAvailability;
        List<Availability> saturdayAvailability;
        List<Availability> sundayAvailability;

        List<Requests> mondayRequests;
        List<Requests> tuesdayRequests;
        List<Requests> wednesdayRequests;
        List<Requests> thursdayRequests;
        List<Requests> fridayRequests;
        List<Requests> saturdayRequests;
        List<Requests> sundayRequests;

        List<int> shiftsToFillPerWeekDay = new List<int>();

        List<Shift> mondayShiftsToFill;
        List<Shift> tuesdayShiftsToFill;
        List<Shift> wednesdayShiftsToFill;
        List<Shift> thursdayShiftsToFill;
        List<Shift> fridayShiftsToFill;
        List<Shift> saturdayShiftsToFill;
        List<Shift> sundayShiftsToFill;

        Random random = new Random();

        public ActionResult generateSchedule()
        {

            mondayAvailability = (from available in db.availability
                                  where available.IsAvailableMonday == true
                                  select (available)).ToList<Availability>();
            tuesdayAvailability = (from available in db.availability
                                   where available.IsAvailableTuesday == true
                                   select (available)).ToList<Availability>();
            wednesdayAvailability = (from available in db.availability
                                     where available.IsAvailableWednesday == true
                                     select (available)).ToList<Availability>();
            thursdayAvailability = (from available in db.availability
                                    where available.IsAvailableThursday == true
                                    select (available)).ToList<Availability>();
            fridayAvailability = (from available in db.availability
                                  where available.IsAvailableFriday == true
                                  select (available)).ToList<Availability>();
            saturdayAvailability = (from available in db.availability
                                    where available.IsAvailableSaturday == true
                                    select (available)).ToList<Availability>();
            sundayAvailability = (from available in db.availability
                                  where available.IsAvailableSunday == true
                                  select (available)).ToList<Availability>();

            mondayRequests = (from request in db.request
                              where request.DayID.DayOfWeek.Equals("Monday")
                              select (request)).ToList<Requests>();
            tuesdayRequests = (from request in db.request
                               where request.DayID.DayOfWeek.Equals("Tuesday")
                               select (request)).ToList<Requests>();
            wednesdayRequests = (from request in db.request
                                 where request.DayID.DayOfWeek.Equals("Wednesday")
                                 select (request)).ToList<Requests>();
            thursdayRequests = (from request in db.request
                                where request.DayID.DayOfWeek.Equals("Thursday")
                                select (request)).ToList<Requests>();
            fridayRequests = (from request in db.request
                              where request.DayID.DayOfWeek.Equals("Friday")
                              select (request)).ToList<Requests>();
            saturdayRequests = (from request in db.request
                                where request.DayID.DayOfWeek.Equals("Saturday")
                                select (request)).ToList<Requests>();
            sundayRequests = (from request in db.request
                              where request.DayID.DayOfWeek.Equals("Sunday")
                              select (request)).ToList<Requests>();

            availabilityCountPerWeekDay.Add(mondayAvailability.Count - mondayRequests.Count);
            availabilityCountPerWeekDay.Add(tuesdayAvailability.Count - tuesdayRequests.Count);
            availabilityCountPerWeekDay.Add(wednesdayAvailability.Count - wednesdayRequests.Count);
            availabilityCountPerWeekDay.Add(thursdayAvailability.Count - thursdayRequests.Count);
            availabilityCountPerWeekDay.Add(fridayAvailability.Count - fridayRequests.Count);
            availabilityCountPerWeekDay.Add(saturdayAvailability.Count - saturdayRequests.Count);
            availabilityCountPerWeekDay.Add(sundayAvailability.Count - sundayRequests.Count);

            mondayShiftsToFill = (from shift in db.shift
                                  where shift.DayID.DayOfWeek.Equals("Monday")
                                  select (shift)).ToList<Shift>();
            tuesdayShiftsToFill = (from shift in db.shift
                                   where shift.DayID.DayOfWeek.Equals("Tuesday")
                                   select (shift)).ToList<Shift>();
            wednesdayShiftsToFill = (from shift in db.shift
                                     where shift.DayID.DayOfWeek.Equals("Wednesday")
                                     select (shift)).ToList<Shift>();
            thursdayShiftsToFill = (from shift in db.shift
                                    where shift.DayID.DayOfWeek.Equals("Thursday")
                                    select (shift)).ToList<Shift>();
            fridayShiftsToFill = (from shift in db.shift
                                  where shift.DayID.DayOfWeek.Equals("Friday")
                                  select (shift)).ToList<Shift>();
            saturdayShiftsToFill = (from shift in db.shift
                                    where shift.DayID.DayOfWeek.Equals("Saturday")
                                    select (shift)).ToList<Shift>();
            sundayShiftsToFill = (from shift in db.shift
                                  where shift.DayID.DayOfWeek.Equals("Sunday")
                                  select (shift)).ToList<Shift>();

            shiftsToFillPerWeekDay.Add(mondayShiftsToFill.Count);
            shiftsToFillPerWeekDay.Add(tuesdayShiftsToFill.Count);
            shiftsToFillPerWeekDay.Add(wednesdayShiftsToFill.Count);
            shiftsToFillPerWeekDay.Add(thursdayShiftsToFill.Count);
            shiftsToFillPerWeekDay.Add(fridayShiftsToFill.Count);
            shiftsToFillPerWeekDay.Add(saturdayShiftsToFill.Count);
            shiftsToFillPerWeekDay.Add(sundayShiftsToFill.Count);

            List<int> dayPriorityList = new List<int>();
            dayPriorityList.Add(-1000);

            for (int i = 0; i < 7; i++)
            {
                int priority = availabilityCountPerWeekDay[i] - shiftsToFillPerWeekDay[i];
                dayPriorityList.Add(priority);
            }

            int priorityLevel = 0;
            int dayCounter = 0;

            for (int i = 0; i < dayPriorityList.Count; i++)
            {
                if (dayPriorityList[i] == priorityLevel)
                {
                    scheduleDay(i);
                    dayPriorityList[i] = -1;
                    dayCounter++;
                    i = 0;
                    if (dayCounter >= 7)
                    {
                        return RedirectToAction("Index", "Schedule");
                    }
                }
                if (i == dayPriorityList.Count - 1)
                {
                    i = 0;
                    priorityLevel++;
                }
            }
            return View("Index", "Schedule");
        }

            public void scheduleDay(int day)
            {
                switch (day)
                {
                    case 1:

                        for (int i = 0; i < mondayRequests.Count; i++)
                        {
                            for (int j = 0; j < mondayAvailability.Count; j++)
                            {
                                if (mondayRequests[i].EmployeeID == mondayAvailability[j].EmployeeID)
                                {
                                    mondayAvailability.Remove(mondayAvailability[j]);
                                }
                            }
                        }

                        for (int i = 0; i < mondayShiftsToFill.Count; i++)
                            {
                                int employeeSelectMonday = random.Next(0, mondayAvailability.Count);
                                mondayShiftsToFill[i].EmployeeID = mondayAvailability[employeeSelectMonday].EmployeeID;
                                mondayShiftsToFill[i].Status = "Filled";
                                mondayAvailability.RemoveAt(employeeSelectMonday);
                                db.SaveChanges();
                            }

                        break;

                    case 2:

                        for (int i = 0; i < tuesdayRequests.Count; i++)
                        {
                            for (int j = 0; j < tuesdayAvailability.Count; j++)
                            {
                                if (tuesdayRequests[i].EmployeeID == tuesdayAvailability[j].EmployeeID)
                                {
                                    tuesdayAvailability.Remove(tuesdayAvailability[j]);
                                }
                            }
                        }

                        for (int i = 0; i < tuesdayShiftsToFill.Count; i++)
                            {
                                int employeeSelectTuesday = random.Next(0, tuesdayAvailability.Count);
                                tuesdayShiftsToFill[i].EmployeeID = tuesdayAvailability[employeeSelectTuesday].EmployeeID;
                                tuesdayShiftsToFill[i].Status = "Filled";
                                tuesdayAvailability.RemoveAt(employeeSelectTuesday);
                                db.SaveChanges();
                            }

                        break;

                    case 3:

                        for (int i = 0; i < wednesdayRequests.Count; i++)
                        {
                            for (int j = 0; j < wednesdayAvailability.Count; j++)
                            {
                                if (wednesdayRequests[i].EmployeeID == wednesdayAvailability[j].EmployeeID)
                                {
                                    wednesdayAvailability.Remove(wednesdayAvailability[j]);
                                }
                            }
                        }

                        for (int i = 0; i < wednesdayShiftsToFill.Count; i++)
                            {
                                int employeeSelectWednesday = random.Next(0, wednesdayAvailability.Count);
                                wednesdayShiftsToFill[i].EmployeeID = wednesdayAvailability[employeeSelectWednesday].EmployeeID;
                                wednesdayShiftsToFill[i].Status = "Filled";
                                wednesdayAvailability.RemoveAt(employeeSelectWednesday);
                                db.SaveChanges();
                            }

                        break;

                    case 4:

                        for (int i = 0; i < thursdayRequests.Count; i++)
                        {
                            for (int j = 0; j < thursdayAvailability.Count; j++)
                            {
                                if (thursdayRequests[i].EmployeeID == thursdayAvailability[j].EmployeeID)
                                {
                                    thursdayAvailability.Remove(thursdayAvailability[j]);
                                }
                            }
                        }

                        for (int i = 0; i < thursdayShiftsToFill.Count; i++)
                            {
                                int employeeSelectThursday = random.Next(0, thursdayAvailability.Count);
                                thursdayShiftsToFill[i].EmployeeID = thursdayAvailability[employeeSelectThursday].EmployeeID;
                                thursdayShiftsToFill[i].Status = "Filled";
                                thursdayAvailability.RemoveAt(employeeSelectThursday);
                                db.SaveChanges();
                            }

                        break;

                    case 5:

                        for (int i = 0; i < fridayRequests.Count; i++)
                        {
                            for (int j = 0; j < fridayAvailability.Count; j++)
                            {
                                if (fridayRequests[i].EmployeeID == fridayAvailability[j].EmployeeID)
                                {
                                    fridayAvailability.Remove(fridayAvailability[j]);
                                }
                            }
                        }

                        for (int i = 0; i < fridayShiftsToFill.Count; i++)
                            {
                                int employeeSelectFriday = random.Next(0, fridayAvailability.Count);
                                fridayShiftsToFill[i].EmployeeID = fridayAvailability[employeeSelectFriday].EmployeeID;
                                fridayShiftsToFill[i].Status = "Filled";
                                fridayAvailability.RemoveAt(employeeSelectFriday);
                                db.SaveChanges();
                            }

                        break;

                    case 6:

                        for (int i = 0; i < saturdayRequests.Count; i++)
                        {
                            for (int j = 0; j < saturdayAvailability.Count; j++)
                            {
                                if (saturdayRequests[i].EmployeeID == saturdayAvailability[j].EmployeeID)
                                {
                                    saturdayAvailability.Remove(saturdayAvailability[j]);
                                }
                            }
                        }

                        for (int i = 0; i < saturdayShiftsToFill.Count; i++)
                            {
                                int employeeSelectSaturday = random.Next(0, saturdayAvailability.Count);
                                saturdayShiftsToFill[i].EmployeeID = saturdayAvailability[employeeSelectSaturday].EmployeeID;
                                saturdayShiftsToFill[i].Status = "Filled";
                                saturdayAvailability.RemoveAt(employeeSelectSaturday);
                                db.SaveChanges();
                            }

                        break;

                    case 7:

                        for (int i = 0; i < sundayRequests.Count; i++)
                        {
                            for (int j = 0; j < sundayAvailability.Count; j++)
                            {
                                if (sundayRequests[i].EmployeeID == sundayAvailability[j].EmployeeID)
                                {
                                    sundayAvailability.Remove(sundayAvailability[j]);
                                }
                            }
                        }

                        for (int i = 0; i < sundayShiftsToFill.Count; i++)
                            {
                                int employeeSelectSunday = random.Next(0, sundayAvailability.Count);
                                sundayShiftsToFill[i].EmployeeID = sundayAvailability[employeeSelectSunday].EmployeeID;
                                sundayShiftsToFill[i].Status = "Filled";
                                sundayAvailability.RemoveAt(employeeSelectSunday);
                                db.SaveChanges();
                            }

                        break;

                }
            }

        // GET: Shifts
        public ActionResult Index()
       {

            List<Shift> shifts = new List<Shift>();

            foreach (Shift shift in db.shift)
            {
                    shifts.Add(shift);
            }

            if (shifts.Count() == 0)
            {
                return View(shifts);
            }

            else
            {
                return View(shifts);
            }

            
        }

        // GET: Shifts/Details/5
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

        // GET: Shifts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        public ActionResult AddShift(Shift shift, string Type)
        {
            if (ModelState.IsValid)
            {                                
                db.shift.Add(shift);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(shift);
        }

        // GET: Shifts/Edit/5
        public ActionResult ManagerEdit(int? id)
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

        // POST: Shifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManagerEdit(Shift shift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shift).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shift);
        }

        // GET: Shifts/Delete/5
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

        // POST: Shifts/Delete/5
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
