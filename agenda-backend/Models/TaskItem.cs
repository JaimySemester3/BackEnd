﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace agenda_backend.Models
{
    public class TaskItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        [JsonPropertyName("taskName")]
        public string TaskName { get; set; } = null!;

        [BsonElement("Priority")]
        public int Priority { get; set; }

    }
}
