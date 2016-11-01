using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;

namespace ComputersFactory.Models
{
    public class ComputersFactoryContext : DbContext
    {
        private const string connectionString = "Server=.;Database=ComputersFactory;Integrated Security = true;MultipleActiveResultSets=True;";

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

        public async Task CreateDB()
        {
            using (var ctx = new ComputersFactoryContext())
            {
                await ctx.Manufacturers.ToListAsync();
                await ctx.SaveChangesAsync();
            }
        }
    }
}
