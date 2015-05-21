using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PzProj.Models
{
    /// <summary>
    /// Prosty pomiar, założenie jeden sensor jeden typ pomiaru rejestrowany
    // ręcznie w monitorze
    /// </summary>
    /// 
    [Table("simple_measure_type")]
    public class SimpleMeasureType
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int unique_sensor_id { get; set; }
    }
}