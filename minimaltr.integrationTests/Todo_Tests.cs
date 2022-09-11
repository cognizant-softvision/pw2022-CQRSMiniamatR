using System.Net;
using System.Net.Http.Json;

public class Todo_Tests
{
    [Fact]
    public async Task GetTodos()
    {
        //TODO: use collection instead instanciate each time
        await using var application = new TodoApplication();

        var client = application.CreateClient();
        var todos = await client.GetFromJsonAsync<List<Todo>>("/todos");

        Assert.Empty(todos);
    }

    [Fact]
    public async Task PostTodos()
    {
        await using var application = new TodoApplication();

        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/todos", new Todo { Name = "I want to do this thing tomorrow" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var todos = await client.GetFromJsonAsync<List<Todo>>("/todos");

        var todo = Assert.Single(todos);
        Assert.Equal("I want to do this thing tomorrow", todo.Name);
        Assert.False(todo.IsComplete);
    }

    [Fact]
    public async Task DeleteTodos()
    {
        await using var application = new TodoApplication();

        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/todos", new Todo { Name = "I want to do this thing tomorrow" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var todos = await client.GetFromJsonAsync<List<Todo>>("/todos");

        var todo = Assert.Single(todos);
        Assert.Equal("I want to do this thing tomorrow", todo.Name);
        Assert.False(todo.IsComplete);

        response = await client.DeleteAsync($"/todos/{todo.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        response = await client.GetAsync($"/todos/{todo.Id}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
