using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PzProj.Respons
{
    public class MeasurementResponse
    {
        public int host_id  { get; set; }
        //public int SimpleMeasureTypeId {get; set; }
        public DateTime time { get; set; }
        /// <summary>
        /// Wartość obciążenia
        /// </summary>
        public int value { get; set; }
    }
}