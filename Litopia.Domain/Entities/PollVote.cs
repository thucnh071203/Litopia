using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litopia.Domain.Entities
{
    public class PollVote
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string VoteId { get; set; } = null!;

        [BsonElement("pollId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string PollId { get; set; } = null!;

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string UserId { get; set; } = null!;

        [BsonElement("optionIds")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public List<string> OptionIds { get; set; } = null!;

        [BsonElement("votedAt")]
        [BsonRequired]
        public DateTime VotedAt { get; set; }
    }
}
