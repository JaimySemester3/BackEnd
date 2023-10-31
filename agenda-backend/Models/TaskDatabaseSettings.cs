namespace agenda_backend.Models
{
    public class TaskDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string TasksCollectionName { get; set; } = null!;
    }
}
