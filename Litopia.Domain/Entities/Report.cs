using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReportId { get; set; } = null!;

        [BsonElement("postId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string PostId { get; set; } = null!;

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string UserId { get; set; } = null!;

        [BsonElement("reason")]
        [BsonRequired]
        public string Reason { get; set; } = null!;

        [BsonElement("reportAt")]
        [BsonRequired]
        public DateTime ReportAt { get; set; }

        [BsonElement("status")]
        [BsonRequired]
        public string Status { get; set; } = null!;

        [BsonElement("reviewedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ReviewedBy { get; set; }

        [BsonElement("reviewedAt")]
        public DateTime? ReviewedAt { get; set; }
    }
}
