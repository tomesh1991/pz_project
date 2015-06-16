using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PzProj.Requests
{
    public class AdvMeasurmentRequest
    {
        public int UserId { get; set; }
        public int SimpleMeasurmentTypeId { get; set; }
        public int Frequency { get; set; }
        public int Length { get; set; }
    }
}