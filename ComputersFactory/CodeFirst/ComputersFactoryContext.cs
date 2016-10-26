using CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Processor> Processors { get; set; }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<Videocard> Videocards { get; set; }
        public DbSet<ComputerType> ComputerTypes { get; set; }

    }
}
