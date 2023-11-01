using agenda_backend.Models;

namespace agenda_backend.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAsync();
        Task<TaskItem?> GetAsync(string id);
        Task CreateAsync(TaskItem newTaskItem);
        Task UpdateAsync(string id, TaskItem updatedTaskItem);
        Task RemoveAsync(string id);
    }
}
