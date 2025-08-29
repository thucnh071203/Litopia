using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class GroupChatRole
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GroupRoleId { get; set; } = null!;

        [BsonElement("roleName")]
        [BsonRequired]
        public string RoleName { get; set; } = null!;

        [BsonElement("description")]
        public string? Description { get; set; }
    }
}
