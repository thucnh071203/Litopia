using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; } = null!;

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string UserId { get; set; } = null!;

        [BsonElement("content")]
        [BsonRequired]
        public string Content { get; set; } = null!;

        [BsonElement("images")]
        public List<string>? Images { get; set; }

        [BsonElement("taggedFriends")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string>? TaggedFriends { get; set; }

        [BsonElement("lastModifiedAt")]
        [BsonRequired]
        public DateTime LastModifiedAt { get; set; }

        [BsonElement("status")]
        [BsonRequired]
        public string Status { get; set; } = null!;

        [BsonElement("isDeleted")]
        [BsonRequired]
        public bool IsDeleted { get; set; }
    }
}
