using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace PzProj.Requests
{
    public class MeasurementRequest  
    {
        public virtual  HostRequest Host {get ; set; }
        
        // Id sensora, przydzielany dla każdego sensora osobno, niezmienny
        public int SensorUniqueId { get; set; }

        // Wartość procentowa obciążenia
        public int Value { get; set; }
    }
}