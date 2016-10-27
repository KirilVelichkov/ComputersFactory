using MongoDB.Bson.Serialization.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
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

        public double MhZ { get; set; }

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