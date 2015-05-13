using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace PzProj.Requests
{
    public class MeasurementRequest : HttpRequestMessage 
    {
        public virtual  HostRequest host {get ; set; }
        public int SensorUniqueId { get; set; }
        public int Value { get; set; }
    }
}