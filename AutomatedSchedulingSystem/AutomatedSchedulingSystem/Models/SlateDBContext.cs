using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AutomatedSchedulingSystem.Models
{
    public class SlateDBContext : DbContext
    {

        public SlateDBContext() : base("DefaultConnection")
        {

        }
        public DbSet<Employee> employee { get; set; }
        public DbSet<Restaurant> restaurant { get; set; }
        public DbSet<Availability> availability { get; set; }
        public DbSet<Schedule> schedule { get; set; }
        public DbSet<Day> day { get; set; }
        public DbSet<Shift> shift { get; set; }
        public DbSet<Requests> request { get; set; }
        public DbSet<Messages> message { get; set; }
        public DbSet<MessageTransactions> messageTransaction { get; set; }

    }
}