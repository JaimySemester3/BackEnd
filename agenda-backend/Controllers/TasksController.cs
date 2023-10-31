﻿using Microsoft.AspNetCore.Mvc;
using agenda_backend.Models;
using agenda_backend.Services;

namespace agenda_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TasksService _tasksService;

    public TasksController(TasksService tasksService) =>
        _tasksService = tasksService;

    [HttpGet]
    public async Task<List<TaskItem>> Get() =>
        await _tasksService.GetAsync();

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