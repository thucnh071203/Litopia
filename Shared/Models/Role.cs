﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Shared.Models
{
    public class Role
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RoleId { get; set; }

        public string? RoleName { get; set; }
    }
}
