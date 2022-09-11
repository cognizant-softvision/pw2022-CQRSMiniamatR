using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minimalTR.Ping;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations(true, true));//Enable swagger annotations

builder.Services.AddMediatR(x => x.AsScoped(), Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello programmers week!");

app.MapGet("/todoitems", async (TodoDb db) =>
    await db.Todos.Select(x => new TodoItemDTO(x)).ToListAsync()
    );

app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(new TodoItemDTO(todo))
            : Results.NotFound())
    .Produces<Todo>(StatusCodes.Status200OK)
    .WithTags("TodoGroup")
    .WithMetadata(new SwaggerOperationAttribute(summary: "Get a todo item by id", description: "A longer description goes here"));

app.MapPost("/todoitems", CreateTodoItem)
    .WithTags("TodoGroup");

app.MapPut("/todoitems/{id}", UpdateTodoItem)
    .WithTags("TodoGroup");

app.MapDelete("/todoitems/{id}", DeleteTodoItem)
    .WithTags("TodoGroup");

//TODO: check metadata provider fromhttps://github.com/DamianEdwards/MinimalApis.Extensions/blob/main/src/MinimalApis.Extensions/Metadata/EndpointMetadataProviderApiDescriptionProvider.cs#L135
app.MapGet("/hello", (MessageDto message) => $"hello {message.Message}")
     .WithTags("Ping");

//Map get for ping action to return pong
// app.MapGet("/ping", async ([FromQuery] string message, IMediator mediator) => await mediator.Send(new PingRequest { Message = message }));
app.MediatorMapGet("/ping", ([FromQuery(Name = "message") ]string message) => new PingRequest { Message = message })
    .WithTags("Ping");

// app.MediatorMapGet("/otherPing", (PingRequest request) => request)
//     .WithTags("Ping");


//New section to register programmers

app.Run();

#region MappedTodo
	//Some mapped items
	static async Task<IResult> CreateTodoItem(TodoItemDTO todoItemDTO, TodoDb db)
	{
	    var todoItem = new Todo
	    {
	        IsComplete = todoItemDTO.IsComplete,
	        Name = todoItemDTO.Name
	    };
	
	    db.Todos.Add(todoItem);
	    await db.SaveChangesAsync();
	
	    return Results.Created($"/todoitems/{todoItem.Id}", new TodoItemDTO(todoItem));
	}
	
	static async Task<IResult> UpdateTodoItem(int id, TodoItemDTO todoItemDTO, TodoDb db)
	{
	    var todo = await db.Todos.FindAsync(id);
	
	    if (todo is null) return Results.NotFound();
	
	    todo.Name = todoItemDTO.Name;
	    todo.IsComplete = todoItemDTO.IsComplete;
	
	    await db.SaveChangesAsync();
	
	    return Results.NoContent();
	}
	
	static async Task<IResult> DeleteTodoItem(int id, TodoDb db)
	{
	    if (await db.Todos.FindAsync(id) is Todo todo)
	    {
	        db.Todos.Remove(todo);
	        await db.SaveChangesAsync();
	        return Results.Ok(new TodoItemDTO(todo));
	    }
	
	    return Results.NotFound();
	}
#endregion

