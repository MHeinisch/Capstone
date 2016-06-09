using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutomatedSchedulingSystem.Models
{
    public class Requests
    {

        [Key]
        public int ID { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; }
        //Approved Denied Pending
        
        public virtual Employee EmployeeID { get; set; }        
       
        public virtual Day DayID { get; set; }        

    }
}