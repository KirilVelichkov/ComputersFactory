using System.Collections.Generic;

namespace CodeFirst.Models
{
    public class Computer
    {
        private ICollection<ComputerType> computerType;
        private ICollection<Processor> processor;
        private ICollection<Memory> memory;
        private ICollection<Videocard> videocard;
        public Computer()
        {
            this.processor = new HashSet<Processor>();
            this.memory = new HashSet<Memory>();
            this.videocard = new HashSet<Videocard>();
            this.computerType = new HashSet<ComputerType>();
        }

        public int Id { get; set; }

        public int ComputerTypeId { get; set; }
        //public string ComputerType { get; set; }

        public int ProcessorId { get; set; }

        public int MemoryId { get; set; }

        public int VideocardId { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<ComputerType> ComputerType { get; set; }
        public virtual ICollection<Processor> Processor { get; set; }
        public virtual ICollection<Memory> Memory { get; set; }
        public virtual ICollection<Videocard> Videocard { get; set; }
    }
}