using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class GoalBook
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GoalBookId { get; set; } = null!;

        [BsonElement("readingGoalId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string ReadingGoalId { get; set; } = null!;

        [BsonElement("bookId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string BookId { get; set; } = null!;

        [BsonElement("status")]
        [BsonRequired]
        public string Status { get; set; } = null!;

        [BsonElement("completedAt")]
        public DateTime? CompletedAt { get; set; }
    }
}
