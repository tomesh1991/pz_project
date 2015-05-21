using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiPZ.Models;

namespace WebApiPZ.Controllers
{
    public class measurementsController : ApiController
    {
        private pzProjDBEntities db = new pzProjDBEntities();

        // GET api/measurements
        public IQueryable<measurements> Getmeasurements()
        {
            return db.measurements;
        }

        // GET api/measurements/5
        [ResponseType(typeof(measurements))]
        public IHttpActionResult Getmeasurements(int id)
        {
            measurements measurements = db.measurements.Find(id);
            if (measurements == null)
            {
                return NotFound();
            }

            return Ok(measurements);
        }

        // PUT api/measurements/5
        public IHttpActionResult Putmeasurements(int id, measurements measurements)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != measurements.id)
            {
                return BadRequest();
            }

            db.Entry(measurements).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!measurementsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/measurements
        [ResponseType(typeof(measurements))]
        public IHttpActionResult Postmeasurements(measurements measurements)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.measurements.Add(measurements);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (measurementsExists(measurements.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = measurements.id }, measurements);
        }

        // DELETE api/measurements/5
        [ResponseType(typeof(measurements))]
        public IHttpActionResult Deletemeasurements(int id)
        {
            measurements measurements = db.measurements.Find(id);
            if (measurements == null)
            {
                return NotFound();
            }

            db.measurements.Remove(measurements);
            db.SaveChanges();

            return Ok(measurements);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool measurementsExists(int id)
        {
            return db.measurements.Count(e => e.id == id) > 0;
        }
    }
}