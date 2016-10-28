using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ComputersFactory.Models
{
    public class ComputersFactoryContext : DbContext
    {
        private const string connectionString = "ComputersFactory";

        public ComputersFactoryContext()
            : base(connectionString)
        {
        }

        public virtual IDbSet<Manufacturer> Manufacturers { get; set; }
        public virtual IDbSet<Computer> Computers { get; set; }
        public virtual IDbSet<Processor> Processors { get; set; }
        public virtual IDbSet<Memorycard> Memorycards { get; set; }
        public virtual IDbSet<Videocard> Videocards { get; set; }
        public virtual IDbSet<ComputerType> ComputerTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
