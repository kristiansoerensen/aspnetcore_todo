using ServiceLayer;
using ServiceLayer.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ITodoService, TodoService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Todo API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1"));
}

app.MapGet("/api/todoitems", (ITodoService todoService, bool isCompleted) => {
    return Results.Ok(todoService.GetTodos().Where(t => t.IsCompleted == isCompleted));
});

app.MapGet("/api/todoitems/all", (ITodoService todoService) => {
    return Results.Ok(todoService.GetTodos());
});

app.MapGet("/api/todo/{id}", (ITodoService todoService, int id) => {
    TodoItem todoItem = todoService.GetTodoById(id);
    if (todoItem != null)
    {
        return Results.Ok(todoService.GetTodoById(id));

    }
    return Results.NotFound();
});

app.MapPut("/api/todo", (ITodoService todoService, TodoItem todoItem) => {
    if (todoService.UpdateTodoItem(todoItem))
    {
        return Results.Ok();

    }
    return Results.NotFound();
});

app.MapDelete("/api/todo/{id}", (ITodoService todoService, int id) => {
    todoService.DeleteTodoItem(id);
    return Results.Ok();
});

app.MapPost("/api/todo", (ITodoService todoService, TodoItem todoItem) => {
    return Results.Ok(todoService.AddTodoItem(todoItem));
});

app.Run();