using MongoDB.Bson.Serialization.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputersFactory.Models
{
    [BsonIgnoreExtraElements]
    public class Memorycard 
    {
        private ICollection<Computer> computers;

        public Memorycard()
        {
            this.computers = new HashSet<Computer>();
        }

        [BsonIgnore]
        public int Id { get; set; }

        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }

        [Range(0, 10)]
        public double MhZ { get; set; }

        [MaxLength(15)]
        public string Capacity { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Computer> Computers
        {
            get { return this.computers; }
            set { this.computers = value; }
        }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}