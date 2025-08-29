using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class GroupChat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GroupId { get; set; } = null!;

        [BsonElement("groupName")]
        [BsonRequired]
        public string GroupName { get; set; } = null!;

        [BsonElement("creatorId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string CreatorId { get; set; } = null!;

        [BsonElement("createdDate")]
        [BsonRequired]
        public DateTime CreatedDate { get; set; }

        [BsonElement("isApprovalRequired")]
        [BsonRequired]
        public bool IsApprovalRequired { get; set; }

        [BsonElement("isActive")]
        [BsonRequired]
        public bool IsActive { get; set; }

        [BsonElement("isPublic")]
        [BsonRequired]
        public bool IsPublic { get; set; }

        [BsonElement("pendingMembers")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string>? PendingMembers { get; set; }
    }
}
