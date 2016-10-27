using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    [BsonIgnoreExtraElements]
    public class Computer
    {
        [BsonIgnore]
        public int Id { get; set; }

        [ForeignKey("ComputerType")]
        public int ComputerTypeId { get; set; }

        [ForeignKey("Processor")]
        public int ProcessorId { get; set; }

        [ForeignKey("Memorycard")]
        public int MemorycardId { get; set; }

        [ForeignKey("Videocard")]
        public int VideocardId { get; set; }

        public decimal Price { get; set; }

        public virtual ComputerType ComputerType { get; set; }
        public virtual Processor Processor { get; set; }
        public virtual Memorycard Memorycard { get; set; }
        public virtual Videocard Videocard { get; set; }
    }
}