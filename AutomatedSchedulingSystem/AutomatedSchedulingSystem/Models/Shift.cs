using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutomatedSchedulingSystem.Models
{
    public class Shift
    {

        [Key]
        public int ID { get; set; }

        [Display(Name = "Start Time")]
        public string StartTime { get; set; }

        [Display(Name = "End Time")]
        public string EndTime { get; set; }
        
        [Display(Name = "Shift Type")]
        public string Type { get; set; }
        //Open Close Neutral

        [Display(Name = "Shift Status")]
        public string Status { get; set; }
        //Filled Dropped Pending
        
        public virtual Employee EmployeeID { get; set; }        
       
        public virtual Day DayID { get; set; }       

    }
}