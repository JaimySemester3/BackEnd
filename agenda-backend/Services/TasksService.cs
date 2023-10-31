using agenda_backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace agenda_backend.Services
{
    public class TasksService
    {
        private readonly IMongoCollection<TaskItem> _taskItemsCollection;

        public TasksService(
            IOptions<TaskDatabaseSettings> taskDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                taskDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                taskDatabaseSettings.Value.DatabaseName);

            _taskItemsCollection = mongoDatabase.GetCollection<TaskItem>(
                taskDatabaseSettings.Value.TasksCollectionName);
        }

        public async Task<List<TaskItem>> GetAsync() =>
            await _taskItemsCollection.Find(_ => true).ToListAsync();

        public async Task<TaskItem?> GetAsync(string id) =>
            await _taskItemsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TaskItem newTaskItem) =>
            await _taskItemsCollection.InsertOneAsync(newTaskItem);

        public async Task UpdateAsync(string id, TaskItem updatedTaskItem) =>
            await _taskItemsCollection.ReplaceOneAsync(x => x.Id == id, updatedTaskItem);

        public async Task RemoveAsync(string id) =>
            await _taskItemsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
