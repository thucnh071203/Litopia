using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class FavoriteBook
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FavoriteBookId { get; set; } = null!;

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string UserId { get; set; } = null!;

        [BsonElement("bookId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string BookId { get; set; } = null!;

        [BsonElement("notes")]
        public string? Notes { get; set; }

        [BsonElement("likedAt")]
        [BsonRequired]
        public DateTime LikedAt { get; set; }
    }
}
