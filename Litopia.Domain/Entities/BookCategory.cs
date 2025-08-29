using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class BookCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; } = null!;

        [BsonElement("categoryName")]
        [BsonRequired]
        public string CategoryName { get; set; } = null!;

        [BsonElement("description")]
        public string? Description { get; set; }
    }
}
