using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using PzProj.Models;
using PzProj.Requests;

namespace PzProj.Controllers
{
    public class MeasurementsController
     : ApiController
    {
        private PzProjContext db = new PzProjContext();

        // GET api/Measurements     
        public IQueryable<Measurements> GetMeasurements()
        {
            return db.Measurements;
        }

        // GET api/Hosts/5
        [ResponseType(typeof(Measurements))]
        public IHttpActionResult GetMeasurements(int id)
        {
            Measurements ms = db.Measurements.Find(id);
            if (ms == null)
            {
                return NotFound();
            }

            return Ok(ms);
        }

        // POST api/Users
        [ResponseType(typeof(Users))]
        public IHttpActionResult PostMeasurements(MeasurementRequest item)
        {
            if (item == null)
            {
                return BadRequest(ModelState);
            }

            Measurements meas = new Measurements();

            Hosts host = db.Hosts.FirstOrDefault(h => h.unique_id ==  item.host.unique_id);

            if (host == null)
            {
                host = new Hosts { unique_id = item.host.unique_id };
                db.Hosts.Add(host);
            }

            if(host.name != item.host.name)
                host.name = item.host.name;

            if(host.ip_addr != item.host.ip_addr)
                host.ip_addr = item.host.ip_addr;

            
            meas.host = host;
            meas.load_cpu = item.load_cpu;
            meas.load_mem = item.load_mem;

            db.Measurements.Add(meas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = meas.id }, meas);
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
    }
}