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
    public class hostsController : ApiController
    {
        private pzProjDBEntities db = new pzProjDBEntities();

        // GET api/hosts
        public IQueryable<hosts> Gethosts()
        {
            return db.hosts;
        }

        // GET api/hosts/5
        [ResponseType(typeof(hosts))]
        public IHttpActionResult Gethosts(int id)
        {
            hosts hosts = db.hosts.Find(id);
            if (hosts == null)
            {
                return NotFound();
            }

            return Ok(hosts);
        }

        // PUT api/hosts/5
        public IHttpActionResult Puthosts(int id, hosts hosts)
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
                if (!hostsExists(id))
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

        // POST api/hosts
        [ResponseType(typeof(hosts))]
        public IHttpActionResult Posthosts(hosts hosts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.hosts.Add(hosts);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (hostsExists(hosts.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hosts.id }, hosts);
        }

        // DELETE api/hosts/5
        [ResponseType(typeof(hosts))]
        public IHttpActionResult Deletehosts(int id)
        {
            hosts hosts = db.hosts.Find(id);
            if (hosts == null)
            {
                return NotFound();
            }

            db.hosts.Remove(hosts);
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

        private bool hostsExists(int id)
        {
            return db.hosts.Count(e => e.id == id) > 0;
        }
    }
}