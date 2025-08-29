using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BookId { get; set; } = null!;

        [BsonElement("title")]
        [BsonRequired]
        public string Title { get; set; } = null!;

        [BsonElement("imageUrl")]
        public string? ImageUrl { get; set; }

        [BsonElement("author")]
        [BsonRequired]
        public string Author { get; set; } = null!;

        [BsonElement("publishedYear")]
        public int? PublishedYear { get; set; }

        [BsonElement("publisher")]
        public string? Publisher { get; set; }

        [BsonElement("isbn")]
        public string? Isbn { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("price")]
        public decimal? Price { get; set; }

        [BsonElement("bookCategoryId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string BookCategoryId { get; set; } = null!;

        [BsonElement("lastModifiedAt")]
        [BsonRequired]
        public DateTime LastModifiedAt { get; set; }

        [BsonElement("isDeleted")]
        [BsonRequired]
        public bool IsDeleted { get; set; }
    }
}
