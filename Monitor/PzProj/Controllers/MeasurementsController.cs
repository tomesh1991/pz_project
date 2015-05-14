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
using PzProj.Requests;
using PzProj.Respons;

namespace PzProj.Controllers
{
    public class MeasurementsController
     : ApiController
    {
        private PzProjContext db = new PzProjContext();

        // GET api/Measurements     
        public IQueryable<Measurement> GetMeasurements()
        {
            return db.Measurements;
        }

        /// <summary>
        /// Get Measure by Measure Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(MeasurementResponse))]
        public IQueryable<MeasurementResponse> GetMeasuresByType(int id)
        {

            return db.Measurements.Where(m => m.SimpleMeasure.id == id)
                .Select(sm => new MeasurementResponse
                {
                    Host = new HostResponse { id = sm.Host.id, ip_addr = sm.Host.ip_addr, name = sm.Host.name },
                    SimpleMeasureTypeId = sm.SimpleMeasure.id,
                    Time = sm.time,
                    Value = sm.Value

                });
        }

        // POST api/Users
        [ResponseType(typeof(Measurement))]

        public IHttpActionResult PostMeasurements(MeasurementRequest item)
        {
            if (item == null || item.host == null || item.SensorUniqueId == null)
            {
                return BadRequest(ModelState);
            }


            var simpleMes = db.SimpleMeasureType.FirstOrDefault(s => s.unique_sensor_id == item.SensorUniqueId);
            if (simpleMes == null)
                return BadRequest(ModelState);

            Measurement meas = new Measurement();

            Host host = db.Hosts.FirstOrDefault(h => h.unique_id == item.host.unique_id);



            if (host == null)
            {
                host = new Host { unique_id = item.host.unique_id };
                db.Hosts.Add(host);
            }

            host.ip_addr = GetClientIp(item);
            host.name = item.host.name;


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