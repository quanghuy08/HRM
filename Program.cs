using Microsoft.EntityFrameworkCore;
using HRM.Models;
using HRM.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HRMContext>(opt => opt.UseInMemoryDatabase("List"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/employee", async (HRMContext db) =>
    await db.Employee.ToListAsync());

app.MapGet("/employee/{id}", async (int id, HRMContext db) =>
    await db.Employee.FindAsync(id)
        is Employee employees
            ? Results.Ok(employees)
            : Results.NotFound());

app.MapPost("/employee", async (Employee todo, HRMContext db) =>
{
    db.Employee.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/employee/{todo.Id}", todo);
});

app.MapPut("/employee/{id}", async (int id, Employee inputTodo, HRMContext db) =>
{
    var todo = await db.Employee.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/employee/{id}", async (int id, HRMContext db) =>
{
    if (await db.Employee.FindAsync(id) is Employee todo)
    {
        db.Employee.Remove(todo);
        await db.SaveChangesAsync();
        return Results.Ok(todo);
    }

    return Results.NotFound();
});

app.Run();