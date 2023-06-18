using Cytocol.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Data.Context
{
    public class CytocolDbContext : DbContext
    {
        //public static readonly OdmDbContext context = new OdmDbContext();
        public CytocolDbContext() : base("name=DefaultConnection") { }

        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Law> Laws { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lawyer>().MapToStoredProcedures();
            modelBuilder.Entity<Law>().MapToStoredProcedures();
            modelBuilder.Entity<Ticket>().MapToStoredProcedures();
            modelBuilder.Entity<User>().MapToStoredProcedures();
        }
    }
}
