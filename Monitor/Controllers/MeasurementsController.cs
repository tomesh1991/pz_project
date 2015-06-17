using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using PzProj.Models;
using DTO.Responses;
using DTO.Requests;

namespace PzProj.Controllers
{

    /// <summary>
    /// Pomiary proste
    /// </summary>
    public class MeasurementsController
     : ApiController
    {
        private PzProjContext db = new PzProjContext();

        /// <summary>
        /// Lista pomiarów prostych / sensorów zarejestrowanych w monitorze
        /// </summary>
        /// <returns></returns>
 
        public IQueryable<MeasureTypeResponse> GetMeasurements()
        {
            return db.SimpleMeasureType
                .Select(sm => 
                    new MeasureTypeResponse 
                    { id = sm.id, 
                        description = sm.description, 
                        name = sm.name });
        }

        /// <summary>
        /// Ostatnie 100 pomiarów prostych zarejestrowanych przez monitor
        /// </summary>
        /// <param name="id">id pomiaru prostego</param>
        /// <returns></returns>
        [ResponseType(typeof(MeasurementResponse))]
        public IQueryable<MeasurementResponse> GetMeasuresByType(int id,string hostName = null)
        {

            var queryable = db.Measurements.Where(m => m.SimpleMeasure.id == id);
               

            if (!string.IsNullOrEmpty(hostName))
            {
                queryable = queryable.Where(x => x.Host.name == hostName);
            }

            return queryable.OrderByDescending(ms => ms.id).Take(100).OrderBy(ms => ms.id).Select(sm => new MeasurementResponse
                {
                    host_id = sm.Host.id,
                    //SimpleMeasureTypeId = sm.SimpleMeasure.id,
                    time = sm.time,
                    value = sm.Value

                });;
        }

        /// <summary>
        /// Post dla sensora
        /// </summary>
        /// <param name="item">pojedynczy pomiar</param>
        /// <returns></returns>
        public IHttpActionResult PostMeasurements(MeasurementRequest item)
        {
            if (item == null)
            {
                return BadRequest(ModelState);
            }


            var simpleMes = db.SimpleMeasureType.FirstOrDefault(s => s.unique_sensor_id == item.SensorUniqueId);
            if (simpleMes == null)
                return BadRequest(ModelState);

            Measurement meas = new Measurement();

            Host host = db.Hosts.FirstOrDefault(h => h.unique_id == item.Host.unique_id);



            if (host == null)
            {
                host = new Host { unique_id = item.Host.unique_id };
                db.Hosts.Add(host);
            }

            host.ip_addr = HttpContext.Current.Request.UserHostAddress;
            host.name = HttpContext.Current.Request.UserHostName;


            meas.Host = host;
            meas.SimpleMeasure = simpleMes;
            meas.time = DateTime.Now;
            meas.Value = item.Value;
            db.Measurements.Add(meas);
            db.SaveChanges();

            return Ok();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HostsExists(int id)
        {
            return db.Hosts.Count(e => e.id == id) > 0;
        }

        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return   ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
            return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }
     }
    
}