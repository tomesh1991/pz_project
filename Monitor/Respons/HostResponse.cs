using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PzProj.Models;

namespace PzProj.Respons
{
    public class HostResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string ip_addr { get; set; }
        public virtual ICollection<HostMeasReposnse> Measurements { get; set; }
    }

    public class HostMeasReposnse
    {
        public int SimpleMeasureType { get; set; }
        public int Value { get; set; }
        public DateTime Time { get; set; }
    }
}
