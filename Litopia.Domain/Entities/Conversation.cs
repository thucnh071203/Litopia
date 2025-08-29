using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class Conversation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ConversationId { get; set; } = null!;

        [BsonElement("type")]
        [BsonRequired]
        public string Type { get; set; } = null!;

        [BsonElement("memberIds")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public List<string> MemberIds { get; set; } = null!;

        [BsonElement("lastMessageId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? LastMessageId { get; set; }

        [BsonElement("updatedAt")]
        [BsonRequired]
        public DateTime UpdatedAt { get; set; }
    }
}
