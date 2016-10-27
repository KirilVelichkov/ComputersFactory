using MongoDB.Bson.Serialization.Attributes;
using System.Collections;
using System.Collections.Generic;

namespace CodeFirst.Models
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

        public string Type { get; set; }

        public virtual ICollection<Computer> Computers
        {
            get { return this.computers; }
            set { this.computers = value; }
        }
    }
}