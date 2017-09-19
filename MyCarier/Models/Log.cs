using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCarier.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Description { get; set; }
    }
}