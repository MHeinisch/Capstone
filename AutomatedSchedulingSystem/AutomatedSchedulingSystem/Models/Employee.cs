using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutomatedSchedulingSystem.Models
{
    public class Employee
    {

        [Key]
        public int ID { get; set; }

        public string UserID { get; set; }

        [Display(Name = "Associate")]
        public string Name { get; set; }

        [Display (Name = "Phone Number")]
        public string Phone { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Position")]
        public string Role { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public virtual Restaurant RestaurantID { get; set; }

    }
}