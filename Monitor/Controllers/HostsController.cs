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
using PzProj.Models;
using PzProj.Respons;

namespace PzProj.Controllers
{
    public class HostsController : ApiController
    {
        private PzProjContext db = new PzProjContext();

        
        /// <summary>
        /// Pobiera wszystkie hosty z ostatnim pomiarem dla każdego typu (dla autoklienta)
        /// </summary>
        /// <returns></returns>
        public IQueryable<HostResponse> GetHosts()
        {
            return db.Hosts.Select(h =>
                new HostResponse
                {
                    id = h.id,
                    ip_addr = h.ip_addr,
                    name = h.name,
                    Measurements = h.Measurements.GroupBy(m => m.SimpleMeasure.id).Select(hmgr => hmgr.OrderByDescending(hmor => hmor.id).FirstOrDefault()).Select(hmel => 
                        new HostMeasReposnse
                        {
                            SimpleMeasureType = hmel.SimpleMeasure.id,
                            Value = hmel.Value,
                            Time = hmel.time

                        }).ToList()

                });
        }

        /// <summary>
        /// Pobiera pommiary dla hosta
        /// </summary>
        /// <param name="id">id hosta</param>
        /// <returns></returns>
        [ResponseType(typeof(HostResponse))]
        public IHttpActionResult GetHosts(int id)
        {
            Host host = db.Hosts.Find(id);
            if (host == null)
            {
                return NotFound();
            
            }

            HostResponse resp = new HostResponse 
            {
             id=host.id,
             ip_addr = host.ip_addr,
             name = host.name,
             Measurements = host.Measurements.Select(hm => new HostMeasReposnse { SimpleMeasureType = hm.SimpleMeasure.id, Value = hm.Value } ).ToList()
            };

            return Ok(resp);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}