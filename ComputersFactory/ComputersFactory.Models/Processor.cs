using MongoDB.Bson.Serialization.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputersFactory.Models
{
    [BsonIgnoreExtraElements]
    public class Processor
    {
        private ICollection<Computer> computers;

        public Processor()
        {
            this.computers = new HashSet<Computer>();
        }

        [BsonIgnore]
        public int Id { get; set; }

        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }

        [MaxLength(50)]
        public string Model { get; set; }

        public double MhZ { get; set; }

        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        public virtual ICollection<Computer> Computers
        {
            get { return this.computers; }
            set { this.computers = value; }
        }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}