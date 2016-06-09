using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutomatedSchedulingSystem.Models
{
    public class Day
    {

        [Key]
        public int ID { get; set; }

        [Display(Name = "Day")]
        public string DayOfWeek { get; set; }

        public string Date { get; set; }

        public virtual Schedule ScheduleID { get; set; }

    }
}