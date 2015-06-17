using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Responses
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