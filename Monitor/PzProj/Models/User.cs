using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PzProj.Models
{   
    [Table("user")]
    public class User
    {
        [Key]
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string mail { get; set; }
        public int superuser { get; set; }
        public string status { get; set; }


    }
}