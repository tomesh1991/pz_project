using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Monitor_kl2.Connection;
using System.Threading.Tasks;
using DTO.Responses;

namespace Monitor_kl2
{
    public class MonitorHost : IMonitorHost
    {

        private string _uri;
        private IEnumerable<HostResponse> _host;
        private IEnumerable<MeasureTypeResponse> _measureTypes;
        private static Random _rand = new Random(Guid.NewGuid().GetHashCode());
        private MonitorConnection _monitorConnection;

        public MonitorHost(string uri)
        {
            _uri = uri;

            _monitorConnection = new MonitorConnection(uri);
            
            /*
            for (int i = 0; i < 100; ++i)
            {
                _hosts.Add(i, new Host { Id = i, CpuUsege = _rand.Next(100), RamUsage = _rand.Next(100), IpAddr = string.Format("192.168.1.{0}", i), Name = string.Format("Host {0}", i), CurrentStatus = Host.Status.Active, Hour = DateTime.Now});

            }*/
        }

        public IEnumerable<HostResponse> GetActualStats()
        {
            try
            {
                GetHosts();
                return _host;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
                return Enumerable.Empty<HostResponse>();
            }
        }


        private async void GetHosts()
        {
            
            _host = await _monitorConnection.GetHosts();
        }

        public async void GetMeasureTypesAsync()
        {
            _measureTypes = await _monitorConnection.GetMeasureTypes();
        }

        public IEnumerable<MeasureTypeResponse> GetMeasureTypes()
        {
            try
            {
                GetMeasureTypesAsync();
                return _measureTypes;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
                return null;
            }
        }
    }
}
