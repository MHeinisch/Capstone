using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutomatedSchedulingSystem.Models
{
    public class Availability
    {

        [Key]
        public int ID { get; set; }   
          
        [Display(Name = "Available Monday")]
        public bool IsAvailableMonday { get; set; }

        [Display(Name = "Available Tuesday")]
        public bool IsAvailableTuesday { get; set; }

        [Display(Name = "Available Wednesday")]
        public bool IsAvailableWednesday { get; set; }

        [Display(Name = "Available Thursday")]
        public bool IsAvailableThursday { get; set; }

        [Display(Name = "Available Friday")]
        public bool IsAvailableFriday { get; set; }

        [Display(Name = "Available Saturday")]
        public bool IsAvailableSaturday { get; set; }

        [Display(Name = "Available Sunday")]
        public bool IsAvailableSunday { get; set; }

        public virtual Employee EmployeeID { get; set; }

    }
}