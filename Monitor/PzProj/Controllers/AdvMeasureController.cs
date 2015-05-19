using System;
using System.Collections.Generic;
using System.Linq;
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

        // nie wiem czy działa, można uprościć
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