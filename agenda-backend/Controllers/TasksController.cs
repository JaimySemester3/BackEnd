using Microsoft.AspNetCore.Mvc;
using agenda_backend.Models;
using agenda_backend.Services;
using agenda_backend.Interfaces;

namespace agenda_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _tasksService;

    public TasksController(ITaskService tasksService) =>
        _tasksService = tasksService;

 [HttpGet]
public async Task<ActionResult<List<TaskItem>>> Get()
{
    var tasks = await _tasksService.GetAsync();

    if (tasks == null || tasks.Count == 0)
    {
        return NotFound();
    }

    return tasks;
}

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<TaskItem>> Get(string id)
    {
        var task = await _tasksService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        return task;
    }

    [HttpPost]
    public async Task<IActionResult> Post(TaskItem newTask)
    {
        await _tasksService.CreateAsync(newTask);

        return CreatedAtAction(nameof(Get), new { id = newTask.Id }, newTask);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, TaskItem updatedTask)
    {
        var task = await _tasksService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        updatedTask.Id = task.Id;

        await _tasksService.UpdateAsync(id, updatedTask);

        return NoContent();
    }

    //ID length testen
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var task = await _tasksService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        await _tasksService.RemoveAsync(id);

        return NoContent();
    }
}