using CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    public class ComputersFactoryContext : DbContext
    {
        private const string connectionString = "ComputersFactory";

        public ComputersFactoryContext()
            : base(connectionString)
        {
        }

        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Computer> Computers { get; set; }
        public virtual DbSet<Processor> Processors { get; set; }
        public virtual DbSet<Memorycard> Memorycards { get; set; }
        public virtual  DbSet<Videocard> Videocards { get; set; }
        public virtual DbSet<ComputerType> ComputerTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
