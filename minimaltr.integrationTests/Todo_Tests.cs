using System.Net;
using System.Net.Http.Json;

public class Todo_Tests
{
    [Fact]
    public async Task Gettodoitems()
    {
        await using var application = new TodoApplication();

        var client = application.CreateClient();
        var todoitems = await client.GetFromJsonAsync<List<Todo>>("/todoitems");

        Assert.Empty(todoitems);
    }

    [Fact]
    public async Task Posttodoitems()
    {
        await using var application = new TodoApplication();

        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/todoitems", new Todo { Name = "I want to do this thing tomorrow" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var todoitems = await client.GetFromJsonAsync<List<Todo>>("/todoitems");

        var todo = Assert.Single(todoitems);
        Assert.Equal("I want to do this thing tomorrow", todo.Name);
        Assert.False(todo.IsComplete);
    }

    [Fact]
    public async Task Deletetodoitems()
    {
        await using var application = new TodoApplication();

        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/todoitems", new Todo { Name = "I want to do this thing tomorrow" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var todoitems = await client.GetFromJsonAsync<List<Todo>>("/todoitems");

        var todo = Assert.Single(todoitems);
        Assert.Equal("I want to do this thing tomorrow", todo.Name);
        Assert.False(todo.IsComplete);

        response = await client.DeleteAsync($"/todoitems/{todo.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        response = await client.GetAsync($"/todoitems/{todo.Id}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
