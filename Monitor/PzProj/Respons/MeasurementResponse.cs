using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PzProj.Respons
{
    public class MeasurementResponse
    {
        public virtual HostResponse Host { get; set; }
        public int SimpleMeasureTypeId {get; set; }
        public DateTime Time { get; set; }
        public int Value { get; set; }
    }
}