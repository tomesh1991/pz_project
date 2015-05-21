using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PzProj.Models
{
    public class PzProjContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public PzProjContext()
            : base("PzProjContext")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public System.Data.Entity.DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<Host> Hosts { get; set; }
        public System.Data.Entity.DbSet<Measurement> Measurements { get; set; }
        public System.Data.Entity.DbSet<SimpleMeasureType> SimpleMeasureType { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Host>()
                 .HasMany(h => h.Measurements)
                 .WithRequired(m => m.Host)
                 .WillCascadeOnDelete(true);

            modelBuilder.Entity<Measurement>().HasRequired(m => m.SimpleMeasure);
            base.OnModelCreating(modelBuilder);
        }

    }




}