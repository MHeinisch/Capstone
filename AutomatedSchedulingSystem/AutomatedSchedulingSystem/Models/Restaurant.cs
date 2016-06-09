using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutomatedSchedulingSystem.Models
{
    public class Restaurant
    {

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        [Display(Name = "Restaurant")]
        public string Phone { get; set; }

        [Display(Name = "Location")]
        public string Address { get; set; }

    }
}