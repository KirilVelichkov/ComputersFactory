using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputersFactory.Models
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