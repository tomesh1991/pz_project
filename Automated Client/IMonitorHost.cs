using DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor_kl2
{
    public interface IMonitorHost
    {
        IEnumerable<MeasureTypeResponse> GetMeasureTypes();
        IEnumerable<HostResponse> GetActualStats();
    }
}
