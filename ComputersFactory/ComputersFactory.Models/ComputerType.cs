using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComputersFactory.Models
{
    [BsonIgnoreExtraElements]
    public class ComputerType
    {
        private ICollection<Computer> computers;

        public ComputerType()
        {
            this.computers = new HashSet<Computer>();
        }

        [BsonIgnore]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Type { get; set; }

        public virtual ICollection<Computer> Computers
        {
            get { return this.computers; }
            set { this.computers = value; }
        }
    }
}