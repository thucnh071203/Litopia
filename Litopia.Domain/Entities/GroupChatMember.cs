using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class GroupChatMember
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GroupMemberId { get; set; } = null!;

        [BsonElement("groupId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string GroupId { get; set; } = null!;

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string UserId { get; set; } = null!;

        [BsonElement("groupRoleId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string GroupRoleId { get; set; } = null!;

        [BsonElement("status")]
        [BsonRequired]
        public string Status { get; set; } = null!;

        [BsonElement("joinedDate")]
        [BsonRequired]
        public DateTime JoinedDate { get; set; }
    }
}
