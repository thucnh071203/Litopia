using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace UserService.Domain.Entities
{
    public class FriendRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RequestId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string SenderId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ReceiverId { get; set; }

        public string? Status { get; set; }
        public DateTime? RequestDate { get; set; }
    }
}
