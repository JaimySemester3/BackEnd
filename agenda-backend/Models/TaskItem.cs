using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace agenda_backend.Models
{
    public class TaskItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string TaskName { get; set; } = null!;

    }
}
