using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Monitor_kl2.Models;
using Monitor_kl2.Connection;
using System.Threading.Tasks;

namespace Monitor_kl2
{
    public class MonitorHost : IMonitorHost
    {
        

        private Dictionary<int, Host> _hosts;
        private static Random _rand = new Random(Guid.NewGuid().GetHashCode());
        

        public MonitorHost(string uri)
        {
            _hosts = new Dictionary<int, Host>();
            
            

            GetHosts(uri);
            
            /*
            for (int i = 0; i < 100; ++i)
            {
                _hosts.Add(i, new Host { Id = i, CpuUsege = _rand.Next(100), RamUsage = _rand.Next(100), IpAddr = string.Format("192.168.1.{0}", i), Name = string.Format("Host {0}", i), CurrentStatus = Host.Status.Active, Hour = DateTime.Now});

            }*/
        }

        public Dictionary<int, Host> ActualStats()
        {
            Refresh();
            return _hosts;
        }

        private void Refresh()
        {
            for (int i = 0; i < 100; ++i)
            {
                var host = _hosts[i];
                host.RamUsage = _rand.Next(100);
                host.CpuUsege = _rand.Next(100);
                host.Hour = DateTime.Now;
            }
        }

        private async void GetHosts(string uri)
        {
            MonitorConnection monitorConnection = new MonitorConnection();
            List<Host> hosts = await monitorConnection.GetHosts(uri);
            int i = 0;
            foreach (Host host in hosts)
            {
                _hosts.Add(i, host);
                i++;
            }
        }
    }
}
