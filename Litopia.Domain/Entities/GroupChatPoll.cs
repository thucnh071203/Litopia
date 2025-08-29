using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class GroupChatPoll
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PollId { get; set; } = null!;

        [BsonElement("groupId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string GroupId { get; set; } = null!;

        [BsonElement("creatorId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string CreatorId { get; set; } = null!;

        [BsonElement("title")]
        [BsonRequired]
        public string Title { get; set; } = null!;

        [BsonElement("options")]
        [BsonRequired]
        public List<object> Options { get; set; } = null!;

        [BsonElement("allowMultiple")]
        [BsonRequired]
        public bool AllowMultiple { get; set; }

        [BsonElement("expiresAt")]
        public DateTime? ExpiresAt { get; set; }

        [BsonElement("createdAt")]
        [BsonRequired]
        public DateTime CreatedAt { get; set; }

        [BsonElement("isClosed")]
        [BsonRequired]
        public bool IsClosed { get; set; }
    }
}
