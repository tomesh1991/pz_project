using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PzProj.Models
{
    /// <summary>
    /// Prosty pomiar, założenie jeden sensor jeden typ pomiaru rejestrowany
    // ręcznie w monitorze
    /// </summary>
    public class SimpleMeasurTypes
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int unique_sensor_id { get; set; }
    }
}