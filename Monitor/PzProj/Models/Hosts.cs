using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PzProj.Models
{   
    [Table("hosts")]
    public class Hosts
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string ip_addr { get; set; }
        public string status { get; set; }

    }
}