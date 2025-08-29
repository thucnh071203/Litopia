using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class Rating
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RatingAtomId { get; set; } = null!;

        [BsonElement("bookId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string BookId { get; set; } = null!;

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string UserId { get; set; } = null!;

        [BsonElement("score")]
        [BsonRequired]
        public int Score { get; set; }

        [BsonElement("comment")]
        public string? Comment { get; set; }

        [BsonElement("ratingAt")]
        [BsonRequired]
        public DateTime RatingAt { get; set; }

        [BsonElement("isDeleted")]
        [BsonRequired]
        public bool IsDeleted { get; set; }
    }
}
