using Xunit;
using agenda_backend.Models;
using System.Reflection;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using MongoDB.Bson;

namespace agenda_backend.Tests
{
    public class TaskItemTests
    {
        [Fact]
        public void TaskItem_IdProperty_HasBsonIdAttribute()
        {
            // Arrange
            var propertyInfo = typeof(TaskItem).GetProperty(nameof(TaskItem.Id));

            // Act
            var attribute = propertyInfo.GetCustomAttribute(typeof(BsonIdAttribute));

            // Assert
            Assert.NotNull(attribute);
        }

        [Fact]
        public void TaskItem_IdProperty_HasBsonRepresentationAttribute()
        {
            // Arrange
            var propertyInfo = typeof(TaskItem).GetProperty(nameof(TaskItem.Id));

            // Act
            var attribute = propertyInfo.GetCustomAttribute(typeof(BsonRepresentationAttribute));

            // Assert
            Assert.NotNull(attribute);
        }

        [Fact]
        public void TaskItem_TaskNameProperty_HasBsonElementAttribute()
        {
            // Arrange
            var propertyInfo = typeof(TaskItem).GetProperty(nameof(TaskItem.TaskName));

            // Act
            var attribute = propertyInfo.GetCustomAttribute(typeof(BsonElementAttribute));

            // Assert
            Assert.NotNull(attribute);
            Assert.Equal("Name", ((BsonElementAttribute)attribute).ElementName);
        }

        [Fact]
        public void TaskItem_TaskNameProperty_HasJsonPropertyNameAttribute()
        {
            // Arrange
            var propertyInfo = typeof(TaskItem).GetProperty(nameof(TaskItem.TaskName));

            // Act
            var attribute = propertyInfo.GetCustomAttribute(typeof(JsonPropertyNameAttribute));

            // Assert
            Assert.NotNull(attribute);
            Assert.Equal("taskName", ((JsonPropertyNameAttribute)attribute).Name);
        }
    }
}
