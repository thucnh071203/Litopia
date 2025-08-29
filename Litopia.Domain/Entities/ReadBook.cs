using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class ReadBook
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReadBookId { get; set; } = null!;

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string UserId { get; set; } = null!;

        [BsonElement("bookId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string BookId { get; set; } = null!;

        [BsonElement("notes")]
        public string? Notes { get; set; }

        [BsonElement("completedAt")]
        [BsonRequired]
        public DateTime CompletedAt { get; set; }

        [BsonElement("sourceGoalId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? SourceGoalId { get; set; }
    }
}
