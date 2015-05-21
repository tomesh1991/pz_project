using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PzProj.Models
{
    [Table("measurement")]
    public class Measurement
    {
        [Key]
        public int id { get; set; }
        public virtual Host Host { get; set; }
        public virtual SimpleMeasureType SimpleMeasure { get; set; }
        public int Value { get; set; }
        public DateTime time { get; set; }
    }
}