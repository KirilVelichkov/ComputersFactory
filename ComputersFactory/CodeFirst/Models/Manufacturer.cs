using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Models
{

    [BsonIgnoreExtraElements]
    public class Manufacturer
    {
        private ICollection<Processor> processors;
        private ICollection<Memorycard> memorycards;
        private ICollection<Videocard> videocards;

        public Manufacturer()
        {
            this.memorycards = new HashSet<Memorycard>();
            this.processors = new HashSet<Processor>();
            this.videocards = new HashSet<Videocard>();
        }

        [BsonIgnore]
        public int Id { get; set; }

        [MaxLength(40)]
        public string Name { get; set; }

        //public int ProcessorId { get; set; }

        //public int MemorycardId { get; set; }

        //public int VideocardId { get; set; }

        public virtual ICollection<Memorycard> Memorycards
        {
            get { return this.memorycards; }
            set { this.memorycards = value; }
        }
        public virtual ICollection<Videocard> Videocards
        {
            get { return this.videocards; }
            set { this.videocards = value; }
        }
        public virtual ICollection<Processor> Processors
        {
            get { return this.processors; }
            set { this.processors = value; }
        }
    }
}