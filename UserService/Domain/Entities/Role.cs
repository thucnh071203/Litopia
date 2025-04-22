using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace UserService.Domain.Entities
{
    public class Role
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RoleId { get; set; }

        public string? RoleName { get; set; }
    }
}
