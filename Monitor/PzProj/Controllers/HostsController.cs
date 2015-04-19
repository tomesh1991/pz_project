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

namespace PzProj.Controllers
{
    public class HostsController : ApiController
    {
        private PzProjContext db = new PzProjContext();

        // GET api/Hosts
        public IQueryable<Hosts> GetHosts()
        {
            return db.Hosts;
        }

        // GET api/Hosts/5
        [ResponseType(typeof(Hosts))]
        public IHttpActionResult GetHosts(int id)
        {
            Hosts hosts = db.Hosts.Find(id);
            if (hosts == null)
            {
                return NotFound();
            }

            return Ok(hosts);
        }

        // PUT api/Hosts/5
        public IHttpActionResult PutHosts(int id, Hosts hosts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hosts.id)
            {
                return BadRequest();
            }

            db.Entry(hosts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HostsExists(id))
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

        // POST api/Hosts
        [ResponseType(typeof(Hosts))]
        public IHttpActionResult PostHosts(Hosts hosts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hosts.Add(hosts);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hosts.id }, hosts);
        }

        // DELETE api/Hosts/5
        [ResponseType(typeof(Hosts))]
        public IHttpActionResult DeleteHosts(int id)
        {
            Hosts hosts = db.Hosts.Find(id);
            if (hosts == null)
            {
                return NotFound();
            }

            db.Hosts.Remove(hosts);
            db.SaveChanges();

            return Ok(hosts);
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