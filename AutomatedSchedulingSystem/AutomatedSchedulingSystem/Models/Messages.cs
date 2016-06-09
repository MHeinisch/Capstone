using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutomatedSchedulingSystem.Models
{
    public class Messages
    {

        [Key]
        public int ID { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

    }
}