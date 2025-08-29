using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class FriendRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RequestId { get; set; } = null!;

        [BsonElement("senderId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string SenderId { get; set; } = null!;

        [BsonElement("receiverId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string ReceiverId { get; set; } = null!;

        [BsonElement("status")]
        [BsonRequired]
        public string Status { get; set; } = null!;

        [BsonElement("requestAt")]
        [BsonRequired]
        public DateTime RequestAt { get; set; }
    }
}
