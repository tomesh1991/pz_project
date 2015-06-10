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
        public TimeSpan Frequency { get; set; }
        public TimeSpan Length { get; set; }
    }
}