using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using PzProj.Models;
using PzProj.Respons;

namespace PzProj.Controllers
{
    public class AdvMeasureController : ApiController
    {
        private PzProjContext db = new PzProjContext();


        // GET api/AdvMeasurements
        public IQueryable<AdvanceMeasure> GetAdvMeasurements()
        {
            return db.AdvanceMeasure;
        }

        // GET api/AdvMeasurements/5
        [ResponseType(typeof(AdvanceMeasure))]
        public IHttpActionResult GetAdvMeasurements(int id)
        {
            AdvanceMeasure AdvMeasurements = db.AdvanceMeasure.Find(id);
            if (AdvMeasurements == null)
            {
                return NotFound();
            }

            return Ok(AdvMeasurements);
        }

        // PUT api/AdvMeasurements/5
        public IHttpActionResult PutAdvMeasurements(int id, AdvanceMeasure AdvMeasurements)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != AdvMeasurements.id)
            {
                return BadRequest();
            }

            db.Entry(AdvMeasurements).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvMeasurementsExists(id))
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

        // POST api/AdvMeasurements
        [ResponseType(typeof(AdvanceMeasure))]
        public IHttpActionResult PostAdvMeasurements(AdvanceMeasure AdvMeasurements)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AdvanceMeasure.Add(AdvMeasurements);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = AdvMeasurements.id }, AdvMeasurements);
        }

        // DELETE api/AdvMeasurements/5
        [ResponseType(typeof(AdvanceMeasure))]
        public IHttpActionResult DeleteAdvMeasurements(int id)
        {
            AdvanceMeasure AdvMeasurements = db.AdvanceMeasure.Find(id);
            if (AdvMeasurements == null)
            {
                return NotFound();
            }

            db.AdvanceMeasure.Remove(AdvMeasurements);
            db.SaveChanges();

            return Ok(AdvMeasurements);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdvMeasurementsExists(int id)
        {
            return db.AdvanceMeasure.Count(e => e.id == id) > 0;
        }




        // nie wiem czy działa, można uprościć
        /// <summary>
        /// Pobiera pomiary złożone
        /// </summary>
        /// <param name="id">id pomiaru złożonego</param>
        /// <param name="dateTime">data od kiedy wyliczać</param>
        /// <returns></returns>
        [ResponseType(typeof(MeasurementResponse))]
        public IHttpActionResult GetAdvMeasurements(int id, DateTime dateTime)
        {
            List<MeasurementResponse> measList = new List<MeasurementResponse>();
            AdvanceMeasure advMeasurement = db.AdvanceMeasure.Find(id);
            if (advMeasurement == null)
            {
                return NotFound();
            }
            DateTime fromDateTime = dateTime -  advMeasurement.MeasureLength;
            var res = db.Measurements.Where(m => m.time >= fromDateTime && m.time <= dateTime);


            DateTime tmpFromDate = dateTime - advMeasurement.MeasureLength;
            DateTime tmpToDate = tmpFromDate + advMeasurement.MeasureFrequency;
            while (tmpFromDate < dateTime)
            {

                var avgList = res.Where(m => m.time > tmpFromDate && m.time < tmpToDate)
                    .GroupBy(h => h.Host.id).Select(h => new MeasurementResponse
                    {
                        host_id = h.Key,
                       time = tmpToDate,
                       value = (int)h.Average(am => am.Value)
                    }).ToList();

                measList.AddRange(avgList);

                tmpFromDate = tmpToDate;
                tmpToDate += advMeasurement.MeasureFrequency;
            }

            return Ok(measList.AsQueryable());
        }
    }
}