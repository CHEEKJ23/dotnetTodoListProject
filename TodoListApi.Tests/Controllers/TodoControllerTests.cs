using Xunit;
using Microsoft.EntityFrameworkCore;
using TodoListApi.Controllers;
using TodoListApi.Data;
using TodoListApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TodoListApi.Tests
{
    public class TodoControllerTests
    {
        private TodoContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            return new TodoContext(options);
        }

        [Fact]
        public async Task CreateTodoItem_ShouldReturnBadRequest_WhenTitleIsEmpty()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new TodoController(context);
            var todo = new TodoItem { Title = "" };

            // Act
            var result = await controller.CreateTodoItem(todo);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreateTodoItem_ShouldAddItem_WhenValid()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new TodoController(context);
            var todo = new TodoItem { Title = "Test Todo" };

            // Act
            var result = await controller.CreateTodoItem(todo);

            // Assert
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var item = Assert.IsType<TodoItem>(created.Value);
            Assert.Equal("Test Todo", item.Title);
        }
    }
}
