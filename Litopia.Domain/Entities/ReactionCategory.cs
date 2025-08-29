using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class ReactionCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReactionCategoryId { get; set; } = null!;

        [BsonElement("reactionName")]
        [BsonRequired]
        public string ReactionName { get; set; } = null!;

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("lastModifiedAt")]
        [BsonRequired]
        public DateTime LastModifiedAt { get; set; }

        [BsonElement("isDeleted")]
        [BsonRequired]
        public bool IsDeleted { get; set; }
    }
}
