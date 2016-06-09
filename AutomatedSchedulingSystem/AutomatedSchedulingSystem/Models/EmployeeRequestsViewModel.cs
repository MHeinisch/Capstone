using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutomatedSchedulingSystem.Models
{
    public class EmployeeRequestsViewModel
    {

        [Key]
        public int ID { get; set; }

        public Employee Employees { get; set; }

        public Requests Requests { get; set; }
    }
}