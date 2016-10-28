using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    [BsonIgnoreExtraElements]
    public class Videocard
    {
        private ICollection<Computer> computers;

        public Videocard()
        {
            this.computers = new HashSet<Computer>();
        }

        [BsonIgnore]
        public int Id { get; set; }

        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }

        [MaxLength(50)]
        public string Model { get; set; }

        [MaxLength(15)]
        public string Memory { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Computer> Computers
        {
            get { return this.computers; }
            set { this.computers = value; }
        }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}