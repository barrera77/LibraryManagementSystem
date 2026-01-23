using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

//Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//BOOK 

app.MapGet("/api/books", () =>
{
    return Results.Ok(new { message = "Retrieved all books" });
}).WithTags("Books");

app.MapGet("/api/books/{id}", (Guid id) =>
{
    return Results.Ok(new { message = $"Retrieved book with ID: {id}" });
}).WithTags("Books");

app.MapPost("/api/books", () =>
{
    return Results.Ok(new { message = "Book created successfully" });
}).WithTags("Books");

app.MapPut("/api/books/{id}", (Guid id) =>
{
    return Results.Ok(new { message = $"Book with ID {id} updated successfully" });
}).WithTags("Books");

app.MapDelete("/api/books/{id}", (Guid id) =>
{
    return Results.Ok(new { message = $"Book with ID {id} deleted successfully" });
}).WithTags("Books");

//Reader

app.MapGet("/api/readers", () =>
{
    return Results.Ok(new { message = "Retrieved all readers" });
}).WithTags("Readers");

app.MapGet("/api/readers/{id}", (Guid id) =>
{
    return Results.Ok(new { message = $"Retrieved reader with ID: {id}" });
}).WithTags("Readers");

app.MapPost("/api/readers", () =>
{
    return Results.Ok(new { message = "Reader created successfully" });
}).WithTags("Readers");

app.MapPut("/api/readers/{id}", (Guid id) =>
{
    return Results.Ok(new { message = $"Reader with ID {id} updated successfully" });
}).WithTags("Readers");

app.MapDelete("/api/readers/{id}", (Guid id) =>
{
    return Results.Ok(new { message = $"Reader with ID {id} deleted successfully" });
}).WithTags("Readers");

//Borrowing 
app.MapGet("/api/borrowings", () =>
{
    return Results.Ok(new { message = "Retrieved all borrowings" });
}).WithTags("Borrowings");

app.MapGet("/api/borrowings/{id}", (Guid id) =>
{
    return Results.Ok(new { message = $"Retrieved borrowing with ID: {id}" });
}).WithTags("Borrowings");

app.MapPost("/api/borrowings", () =>
{
    return Results.Ok(new { message = "Borrowing created successfully" });
}).WithTags("Borrowings");

app.MapPut("/api/borrowings/{id}", (Guid id) =>
{
    return Results.Ok(new { message = $"Borrowing with ID {id} updated successfully" });
}).WithTags("Borrowings");

app.MapDelete("/api/borrowings/{id}", (Guid id) =>
{
    return Results.Ok(new { message = $"Borrowing with ID {id} deleted successfully" });
}).WithTags("Borrowings");

app.Run();