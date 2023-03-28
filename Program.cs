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

// Get all Products
app.MapGet("/Product", (AdventureWorksLt2019Context context) =>
{
    List<Product> products = context.Products.ToList();
    return Results.Ok(products);
});

// Get Product by primary key (ProductId)
app.MapGet("/Product/productId", (int productId, AdventureWorksLt2019Context context) =>
{
    Product product = context.Products.Find(productId);
    if (product == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(product);
});

// Get all SalesOrderHeaders
app.MapGet("/SalesOrderHeader", (AdventureWorksLt2019Context context) =>
{
    List<SalesOrderHeader> salesOrderHeaders = context.SalesOrderHeaders.ToList();
    return Results.Ok(salesOrderHeaders);
});

// Get SalesOrderHeader by primary key (SalesOrderId)
app.MapGet("/SalesOrderHeader/salesOrderId", (int salesOrderId, AdventureWorksLt2019Context context) =>
{
    SalesOrderHeader salesOrderHeader = context.SalesOrderHeaders.Find(salesOrderId);
    if (salesOrderHeader == null) return Results.NotFound();
    return Results.Ok(salesOrderHeader);
});

app.Run();