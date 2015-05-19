using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PzProj.Models
{   
    [Table("host")]
    public class Host

    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string ip_addr { get; set; }
        public long unique_id { get; set; }

        public virtual ICollection<Measurement> Measurements { get; set; }
    }
}