using adventure_works_project.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AdventureWorksLt2019Context>(options =>
    options.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Database=AdventureWorksLT2019;Integrated Security=True;TrustServerCertificate=True"));
var app = builder.Build();

/* Create Endpoints */
// Tested both and I get 201 Created.
app.MapPost("/Product/Create", async (Product product, AdventureWorksLt2019Context context) =>
{
    context.Products.Add(product);
    await context.SaveChangesAsync();
    return Results.Created($"/Product/{product.ProductId}", product);
});

app.MapPost("/SalesOrderHeader/Create", async (SalesOrderHeader salesOrderHeader, AdventureWorksLt2019Context context) =>
{
    context.SalesOrderHeaders.Add(salesOrderHeader);
    await context.SaveChangesAsync();
    return Results.Created($"/SalesOrderHeader/{salesOrderHeader.SalesOrderId}", salesOrderHeader);
});

app.Run();