using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutomatedSchedulingSystem.Models
{
    public class MessageTransactions
    {

        [Key]
        public int ID { get; set; }

        public virtual Employee SenderID { get; set; }

        public virtual Employee RecipientID { get; set; }

        public virtual Messages MessageID { get; set; }

    }
}