using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using DTO.Responses;

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
        private object MeasureTypesLock = new object();
        private const int TRYOUTS = 5;
        private const int GETHOSTS_TIMEOUT = 5000;
        private const int GETMEASURETYPES_TIMEOUT = 5000;

        private TaskCompletionSource<IEnumerable<HostResponse>> GetHostsTCS = null;
        private TaskCompletionSource<IEnumerable<MeasureTypeResponse>> GetMeasureTypesTCS = null;
        private string _uri;

        public MonitorConnection(string uri)
        {
            // TODO: Complete member initialization
            _uri = uri;
        }

        private async Task<T> WaitForTask<T>(Task<T> task, int timeout)
        {
            if (await Task.WhenAny(task, Task.Delay(timeout)) != task)
                throw new MonitorConnectionException("Timeout during executing the task "+ task.ToString());
            return task.Result;
        }

        public async Task<IEnumerable<HostResponse>> GetHosts()
        {

            return await GetHostsAsync();
        }

        public async Task<IEnumerable<MeasureTypeResponse>> GetMeasureTypes()
        {

            return await GetMeasureTypesAsync(_uri);
        }

        private async Task<IEnumerable<HostResponse>> GetHostsAsync()
        {
            Task<IEnumerable<HostResponse>> task = InitGetHostsTCS();
            try
            {
                RequestGetHosts(_uri);
                return await WaitForTask<IEnumerable<HostResponse>>(task, GETHOSTS_TIMEOUT);
            }
            finally
            {
                lock (CommunicationLock)
                {
                    GetHostsTCS = null;
                }
            }
        }

        private async Task<IEnumerable<MeasureTypeResponse>> GetMeasureTypesAsync(string uri)
        {
            Task<IEnumerable<MeasureTypeResponse>> task = InitGetMeasureTypesTCS();
            try
            {
                RequestGetMeasureTypes(uri);
                return await WaitForTask<IEnumerable<MeasureTypeResponse>>(task, GETMEASURETYPES_TIMEOUT);
            }
            finally
            {
                lock (MeasureTypesLock)
                {
                    GetMeasureTypesTCS = null;
                }
            }
        }

        private Task<IEnumerable<HostResponse>> InitGetHostsTCS()
        {
            lock (CommunicationLock)
            {
                if (GetHostsTCS != null)
                    throw new MonitorConnectionException("Cannot start GetHosts task!");

                GetHostsTCS = new TaskCompletionSource<IEnumerable<HostResponse>>();
                return Task<IEnumerable<HostResponse>>.Factory.StartNew(() =>
                    GetHostsTCS.Task.Result
                );
            }
        }


        private Task<IEnumerable<MeasureTypeResponse>> InitGetMeasureTypesTCS()
        {
            lock (MeasureTypesLock)
            {
                if (GetMeasureTypesTCS != null)
                    throw new MonitorConnectionException("Cannot start GetMeasureTypes task!");

                GetMeasureTypesTCS = new TaskCompletionSource<IEnumerable<MeasureTypeResponse>>();
                return Task<IEnumerable<MeasureTypeResponse>>.Factory.StartNew(() =>
                    GetMeasureTypesTCS.Task.Result
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

        private void RequestGetMeasureTypes(string uri)
        {
            lock (MeasureTypesLock)
            {
                DownloadMeasureTypes(uri);
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
                    IEnumerable<HostResponse> hosts = await response.Content.ReadAsAsync<IEnumerable<HostResponse>>();
                    GetHostsTCS.TrySetResult(hosts);
                }
            }
        }

        private async void DownloadMeasureTypes(string address)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/measurements/");
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<MeasureTypeResponse> measTypes = await response.Content.ReadAsAsync<IEnumerable<MeasureTypeResponse>>();
                    GetMeasureTypesTCS.TrySetResult(measTypes);
                }
            }

        }
    }
}
