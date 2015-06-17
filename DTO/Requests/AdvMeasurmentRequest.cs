using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Requests
{
    public class AdvMeasurmentRequest
    {
        public int UserId { get; set; }
        public int SimpleMeasurmentTypeId { get; set; }
        public int Frequency { get; set; }
        public int Length { get; set; }
    }
}