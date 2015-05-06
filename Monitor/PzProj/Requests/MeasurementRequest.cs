using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PzProj.Requests
{
    public class MeasurementRequest
    {
        public virtual  HostRequest host {get ; set; }
        public int load_cpu { get; set; }
        public int load_mem { get; set; }

    }
}