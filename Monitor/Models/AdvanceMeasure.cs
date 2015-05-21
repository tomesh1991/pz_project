using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PzProj.Models
{
    public class AdvanceMeasure
    {
        [Key]
        public int id { get; set; }
        public User User { get; set; }
        public SimpleMeasureType SimpleMeasureType { get; set; }
        public TimeSpan MeasureLength { get; set; } //pomiar z np 5 minut
        public TimeSpan MeasureFrequency {get; set; } //co minute
    }
}