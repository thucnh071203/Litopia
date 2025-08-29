using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class ReadingGoal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReadingGoalId { get; set; } = null!;

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string UserId { get; set; } = null!;

        [BsonElement("goalType")]
        [BsonRequired]
        public string GoalType { get; set; } = null!;

        [BsonElement("targetCount")]
        public int? TargetCount { get; set; }

        [BsonElement("startDate")]
        [BsonRequired]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        [BsonRequired]
        public DateTime EndDate { get; set; }

        [BsonElement("createdAt")]
        [BsonRequired]
        public DateTime CreatedAt { get; set; }

        [BsonElement("isActive")]
        [BsonRequired]
        public bool IsActive { get; set; }
    }
}
