using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = null!;

        [BsonElement("role")]
        [BsonRequired]
        public string Role { get; set; } = null!;

        [BsonElement("fullName")]
        [BsonRequired]
        public string FullName { get; set; } = null!;

        [BsonElement("avatar")]
        public string? Avatar { get; set; }

        [BsonElement("username")]
        [BsonRequired]
        public string Username { get; set; } = null!;

        [BsonElement("password")]
        [BsonRequired]
        public string Password { get; set; } = null!;

        [BsonElement("email")]
        [BsonRequired]
        public string Email { get; set; } = null!;

        [BsonElement("emailConfirmed")]
        [BsonRequired]
        public bool EmailConfirmed { get; set; }

        [BsonElement("phone")]
        public string? Phone { get; set; }

        [BsonElement("phoneConfirmed")]
        public bool? PhoneConfirmed { get; set; }

        [BsonElement("dateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        [BsonElement("gender")]
        public string? Gender { get; set; }

        [BsonElement("bio")]
        public string? Bio { get; set; }

        [BsonElement("createdDate")]
        [BsonRequired]
        public DateTime CreatedDate { get; set; }

        [BsonElement("upToAuthor")]
        public bool? UpToAuthor { get; set; }

        [BsonElement("isDeleted")]
        [BsonRequired]
        public bool IsDeleted { get; set; }
    }
}
