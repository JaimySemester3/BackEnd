using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySql.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace agenda_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly PlannerDbContext _dbContext;

        private readonly ILogger<TaskController> _logger;

        public TaskController(PlannerDbContext dbContext, ILogger<TaskController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet(Name = "GetTasks")]
        public ActionResult<IEnumerable<Task>> GetTasks()
        {
            try
            {
                var tasks = _dbContext.Tasks.ToList();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving tasks.");
                return StatusCode(500, "An error occurred while retrieving tasks");
            }
        }

        [HttpPost]
        public IActionResult CreateTask(Task task)
        {
            try
            {
                _dbContext.Tasks.Add(task);
                _dbContext.SaveChanges();
                return CreatedAtAction(nameof(GetTasks), new { id = task.ID }, task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the task.");
                return StatusCode(500, "An error occurred while creating the task.");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            try
            {
                var task = _dbContext.Tasks.Find(id);
                if (task == null)
                {
                    return NotFound();
                }

                _dbContext.Tasks.Remove(task);
                _dbContext.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the task.");
                return StatusCode(500, "An error occurred while deleting the task");
            }
        }



        /*private readonly string connectionstring = "server=localhost;port=3303;database=plannerdatabase;uid=root;pwd=admin";

        private readonly ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTasks")]
        public ActionResult<IEnumerable<Task>> GetTasks()
        {
            var tasks = new List<Task>();
            using (var con = new MySqlConnection(connectionstring))
            using (var cmd = new MySqlCommand("SELECT id, name, iscompleted FROM task", con))
            {
                try
                {
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new Task(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetBoolean(2)
                            ));
                        }
                    }
                    con.Close();
                    return Ok(tasks);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while retrieving tasks. SQL Query: {SqlQuery}", cmd.CommandText);

                    return StatusCode(500, "An error occurred while retrieving tasks. Please try again later.");
                }

            }
        }
        [HttpPost(Name = "AddTask")]
        public IActionResult Create([FromBody] Task task)
        {
            try
            {
                using (var con = new MySqlConnection(connectionstring))
                {
                    con.Open();
                    var cmd = new MySqlCommand("INSERT INTO Task (Name, IsCompleted) VALUES (@Name, @IsCompleted)", con);
                    cmd.Parameters.AddWithValue("@Name", task.Name);
                    cmd.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return CreatedAtRoute("GetTasks", new { id = task.ID }, task);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the task.");
                return StatusCode(500);
            }
        }*/
    }
}


