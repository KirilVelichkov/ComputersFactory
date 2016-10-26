using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Models
{
    public class Manufacturer
    {
        private ICollection<Computer> computer;
        private ICollection<Processor> processor;
        private ICollection<Memory> memory;
        private ICollection<Videocard> videocard;
        public Manufacturer()
        {
            this.computer = new HashSet<Computer>();
            this.processor = new HashSet<Processor>();
            this.memory = new HashSet<Memory>();
            this.videocard = new HashSet<Videocard>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int ComputerId { get; set; }

        public int ProcessorId { get; set; }

        public int MemoryId { get; set; }

        public int VideocardId { get; set; }

        public virtual ICollection<Computer> Computer { get; set; }
        public virtual ICollection<Processor> Processor { get; set; }
        public virtual ICollection<Memory> Memory { get; set; }
        public virtual ICollection<Videocard> Videocard { get; set; }

    }
}
