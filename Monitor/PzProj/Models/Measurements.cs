using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PzProj.Models
{
    [Table("hosts")]
    public class Measurements
    {
        [Key]
        public int id { get; set; }
        public virtual Hosts host { get; set; }
        public virtual SimpleMeasurTypes simple { get; set; }
        public int Value { get; set; }
        public DateTime time { get; set; }
    }
}