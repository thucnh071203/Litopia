using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class Hashtag
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string HashtagId { get; set; } = null!;

        [BsonElement("postId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string PostId { get; set; } = null!;

        [BsonElement("name")]
        [BsonRequired]
        public string Name { get; set; } = null!;
    }
}
