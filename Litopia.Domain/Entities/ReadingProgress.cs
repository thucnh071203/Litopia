using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class ReadingProgress
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProgressId { get; set; } = null!;

        [BsonElement("readingId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string ReadingId { get; set; } = null!;

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string UserId { get; set; } = null!;

        [BsonElement("milestoneIds")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string>? MilestoneIds { get; set; }

        [BsonElement("updatedAt")]
        [BsonRequired]
        public DateTime UpdatedAt { get; set; }
    }
}
