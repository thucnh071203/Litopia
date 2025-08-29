using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MessageId { get; set; } = null!;

        [BsonElement("conversationId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string ConversationId { get; set; } = null!;

        [BsonElement("senderId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string SenderId { get; set; } = null!;

        [BsonElement("content")]
        [BsonRequired]
        public string Content { get; set; } = null!;

        [BsonElement("media")]
        public List<string>? Media { get; set; }

        [BsonElement("createdAt")]
        [BsonRequired]
        public DateTime CreatedAt { get; set; }

        [BsonElement("isRead")]
        [BsonRequired]
        public bool IsRead { get; set; }

        [BsonElement("isDeleted")]
        [BsonRequired]
        public bool IsDeleted { get; set; }
    }
}
