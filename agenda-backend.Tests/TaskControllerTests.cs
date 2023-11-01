using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using agenda_backend.Controllers;
using agenda_backend.Models;
using agenda_backend.Interfaces;

namespace agenda_backend.Tests
{
    public class TaskControllerTests
    {
        [Fact]
        public async Task Get_ReturnsListOfTasks()
        {
            // Arrange
            var taskServiceMock = new Mock<ITaskService>();
            var expectedTasks = new List<TaskItem> { new TaskItem { Id = "1", TaskName = "Task 1" }, new TaskItem { Id = "2", TaskName = "Task 2" } };
            taskServiceMock.Setup(service => service.GetAsync()).ReturnsAsync(expectedTasks);

            var controller = new TasksController(taskServiceMock.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<TaskItem>>>(result);
            var model = Assert.IsType<List<TaskItem>>(actionResult.Value);
            Assert.Equal(expectedTasks, model);
        }

        [Fact]
        public async Task Get_NoTasks_ReturnsNotFound()
        {
            // Arrange
            var taskServiceMock = new Mock<ITaskService>();
            taskServiceMock.Setup(service => service.GetAsync()).ReturnsAsync(new List<TaskItem>());

            var controller = new TasksController(taskServiceMock.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsTaskItem()
        {
            // Arrange
            var taskId = "1";
            var expectedTask = new TaskItem { Id = taskId, TaskName = "Task 1" };
            var taskServiceMock = new Mock<ITaskService>();
            taskServiceMock.Setup(service => service.GetAsync(taskId)).ReturnsAsync(expectedTask);

            var controller = new TasksController(taskServiceMock.Object);

            // Act
            var result = await controller.Get(taskId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<TaskItem>>(result);
            var taskItem = Assert.IsType<TaskItem>(actionResult.Value);
            Assert.Equal(expectedTask, taskItem);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var taskId = "123456789012345678901234";
            var taskServiceMock = new Mock<ITaskService>();
            taskServiceMock.Setup(service => service.GetAsync(taskId)).ReturnsAsync((TaskItem?)null);

            var controller = new TasksController(taskServiceMock.Object);

            // Act
            var result = await controller.Get(taskId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Post_CreatesNewTaskAndReturnsCreatedAtAction()
        {
            // Arrange
            var newTask = new TaskItem { TaskName = "New Task" };
            var createdTask = new TaskItem { TaskName = "New Task" };
            var taskServiceMock = new Mock<ITaskService>();
            taskServiceMock.Setup(service => service.CreateAsync(newTask)).Returns(Task.CompletedTask);

            var controller = new TasksController(taskServiceMock.Object);

            // Act
            var result = await controller.Post(newTask);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("Get", createdAtActionResult.ActionName);
            Assert.Equal(createdTask.Id, createdAtActionResult.RouteValues["id"]);
            Assert.Equal(newTask, createdAtActionResult.Value);
        }


        [Fact]
        public async Task Update_WithValidId_UpdatesTaskAndReturnsNoContent()
        {
            // Arrange
            var taskId = "1";
            var updatedTask = new TaskItem { Id = taskId, TaskName = "Updated Task" };
            var taskServiceMock = new Mock<ITaskService>();
            taskServiceMock.Setup(service => service.GetAsync(taskId)).ReturnsAsync(updatedTask);
            taskServiceMock.Setup(service => service.UpdateAsync(taskId, updatedTask)).Returns(Task.CompletedTask);

            var controller = new TasksController(taskServiceMock.Object);

            // Act
            var result = await controller.Update(taskId, updatedTask);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var taskId = "nonexistent";
            var updatedTask = new TaskItem { Id = taskId, TaskName = "Updated Task" };
            var taskServiceMock = new Mock<ITaskService>();
            taskServiceMock.Setup(service => service.GetAsync(taskId)).ReturnsAsync((TaskItem)null);

            var controller = new TasksController(taskServiceMock.Object);

            // Act
            var result = await controller.Update(taskId, updatedTask);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_WithValidId_RemovesTaskAndReturnsNoContent()
        {
            // Arrange
            var taskId = "1";
            var taskServiceMock = new Mock<ITaskService>();
            taskServiceMock.Setup(service => service.GetAsync(taskId)).ReturnsAsync(new TaskItem { Id = taskId, TaskName = "Task 1" });
            taskServiceMock.Setup(service => service.RemoveAsync(taskId)).Returns(Task.CompletedTask);

            var controller = new TasksController(taskServiceMock.Object);

            // Act
            var result = await controller.Delete(taskId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task Delete_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var taskId = "nonexistent";
            var taskServiceMock = new Mock<ITaskService>();
            taskServiceMock.Setup(service => service.GetAsync(taskId)).ReturnsAsync((TaskItem)null);

            var controller = new TasksController(taskServiceMock.Object);

            // Act
            var result = await controller.Delete(taskId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}