using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monitor_kl2.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Monitor_kl2.Connection
{

    public class MonitorConnectionException : Exception
    {
        bool critical;
        public MonitorConnectionException()
        {
        }
        public MonitorConnectionException(string message) : base(message)
        {
        }
        public MonitorConnectionException(string message, bool critical)
            : base(message)
        {
            this.critical = critical;
        }
        public MonitorConnectionException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class MonitorConnection
    {
        private object CommunicationLock = new object();
        private const int TRYOUTS = 5;
        private const int GETHOSTS_TIMEOUT = 5000;
        
        private TaskCompletionSource<List<Host>> GetHostsTCS = null;

        private async Task<T> WaitForTask<T>(Task<T> task, int timeout)
        {
            if (await Task.WhenAny(task, Task.Delay(timeout)) != task)
                throw new MonitorConnectionException("Timeout during executing the task "+ task.ToString());
            return task.Result;
        }

        public async Task<List<Host>> GetHosts(string uri)
        {
            List<Host> result = new List<Host>();
            result = await GetHostsAsync(uri);
            return result;
        }

        public async Task<List<Host>> GetHostsAsync(string uri)
        {
            Task<List<Host>> task = InitGetHostsTCS();
            try
            {
                RequestGetHosts(uri);
                return await WaitForTask<List<Host>>(task, GETHOSTS_TIMEOUT);
            }
            finally
            {
                lock (CommunicationLock)
                {
                    GetHostsTCS = null;
                }
            }
        }

        private Task<List<Host>> InitGetHostsTCS()
        {
            lock (CommunicationLock)
            {
                if (GetHostsTCS != null)
                    throw new MonitorConnectionException("Cannot start GetHosts task!");

                GetHostsTCS = new TaskCompletionSource<List<Host>>();
                return Task<List<Host>>.Factory.StartNew(() =>
                    GetHostsTCS.Task.Result
                );
            }
        }


        private void RequestGetHosts(string uri)
        {
            lock(CommunicationLock)
            {
                DownloadHosts(uri);
            }
        }

        private async void DownloadHosts(string address)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/hosts/");
                if (response.IsSuccessStatusCode)
                {
                    List<Host> hosts = await response.Content.ReadAsAsync<List<Host>>();
                    GetHostsTCS.TrySetResult(hosts);
                }
            }
        }
    }
}
