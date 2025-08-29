using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CommentId { get; set; } = null!;

        [BsonElement("postId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string PostId { get; set; } = null!;

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string UserId { get; set; } = null!;

        [BsonElement("content")]
        [BsonRequired]
        public string Content { get; set; } = null!;

        [BsonElement("lastModifiedAt")]
        [BsonRequired]
        public DateTime LastModifiedAt { get; set; }

        [BsonElement("isDeleted")]
        [BsonRequired]
        public bool IsDeleted { get; set; }
    }
}
