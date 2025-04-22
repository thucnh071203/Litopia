using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel;

namespace UserService.Domain.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string RoleId { get; set; }

        public string FullName { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public string Email { get; set; } = null!;

        [DefaultValue(false)]
        public bool? EmailConfirmed { get; set; }

        public string? Phone { get; set; }

        [DefaultValue(false)]
        public bool? PhoneConfirmed { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }
        public string? Bio { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [DefaultValue(false)]
        public bool? UpToAuthor { get; set; }

        public int? ReportCount { get; set; }
        public string? Address { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? Otp { get; set; }
        public DateTime? OtpCreatedAt { get; set; }

        [DefaultValue(false)]
        public bool? IsDeleted { get; set; }

    }
}
