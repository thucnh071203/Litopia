using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string NotificationId { get; set; } = null!;

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string UserId { get; set; } = null!;

        [BsonElement("referenceId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string ReferenceId { get; set; } = null!;

        [BsonElement("content")]
        [BsonRequired]
        public string Content { get; set; } = null!;

        [BsonElement("referenceType")]
        [BsonRequired]
        public string ReferenceType { get; set; } = null!;

        [BsonElement("isRead")]
        [BsonRequired]
        public bool IsRead { get; set; }

        [BsonElement("createdAt")]
        [BsonRequired]
        public DateTime CreatedAt { get; set; }

        [BsonElement("isDeleted")]
        [BsonRequired]
        public bool IsDeleted { get; set; }
    }
}
