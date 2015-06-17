using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor_kl2.Models
{
    public class HostModel
    {

            public int id { get; set; }
            public string name { get; set; }
            public string ip_addr { get; set; }

            public int SimpleMeasureType { get; set; }
            public int Value { get; set; }
            public DateTime Time { get; set; }

            public StatusEnum Status
            {
                get
                {
                    TimeSpan span = DateTime.Now - Time;
                    if (span > new TimeSpan(0, 5, 0))
                        return StatusEnum.Disabled;
                    else
                        return StatusEnum.Activated;
                }
            }
            public enum StatusEnum
            {
                Disabled,
                Activated,
                AddedToList
            }
    }
}
