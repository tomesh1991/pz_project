using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PzProj.Models
{
    public class MeasurTypes
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}