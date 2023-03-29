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
app.MapGet("/Product", async (AdventureWorksLt2019Context context) =>
{
    List<Product> products = await context.Products.ToListAsync();
    return Results.Ok(products);
});

// Get Products by Id
app.MapGet("/Product/{productId:int}", async (int productId, AdventureWorksLt2019Context context) =>
{
    Product products = await context.Products.FindAsync(productId);
    return Results.Ok(products);
});

// Get all SalesOrderHeaders
app.MapGet("/SalesOrderHeader", (AdventureWorksLt2019Context context) =>
{
    List<SalesOrderHeader> salesOrderHeaders = context.SalesOrderHeaders.ToList();
    return Results.Ok(salesOrderHeaders);
});

// Get SalesOrderHeaders by Id
app.MapGet("/SalesOrderHeader/{salesOrderHeaderId:int}", async (int salesOrderHeaderId, AdventureWorksLt2019Context context) =>
{
    SalesOrderHeader salesOrderHeaders = await context.SalesOrderHeaders.FindAsync(salesOrderHeaderId);
    return Results.Ok(salesOrderHeaders);
});

// Update Product by primary key (ProductId)
app.MapPut("/Product/Update/{productId:int}", async (int productId, Product updatedProduct, AdventureWorksLt2019Context context) =>
{
    Product product = await context.Products.FindAsync(productId);
    if (product == null) return Results.NotFound();

    context.Entry(product).CurrentValues.SetValues(updatedProduct);
    await context.SaveChangesAsync();

    return Results.Ok(product);
});

// Update Product by primary key (ProductId)
app.MapPut("/Product/Update/{productId:int}", async (int productId, Product updatedProduct, AdventureWorksLt2019Context context) =>
{
    Product product = await context.Products.FindAsync(productId);
    if (product == null) return Results.NotFound();

    // Tried using context.Entry(product).CurrentValues.SetValues(updatedProduct); But it would return me a 404 Not Found
    product.Name = updatedProduct.Name;
    product.Color = updatedProduct.Color;
    product.StandardCost = updatedProduct.StandardCost;
    product.ListPrice = updatedProduct.ListPrice;
    product.Size = updatedProduct.Size;
    product.Weight = updatedProduct.Weight;

    await context.SaveChangesAsync();

    return Results.Ok(product);
});

// Update SalesOrderHeader by primary key (SalesOrderId)
app.MapPut("/SalesOrderHeader/Update/{salesOrderId:int}", async (int salesOrderId, SalesOrderHeader updatedSalesOrderHeader, AdventureWorksLt2019Context context) =>
{
    SalesOrderHeader salesOrderHeader = await context.SalesOrderHeaders.FindAsync(salesOrderId);
    if (salesOrderHeader == null) return Results.NotFound();

    context.Entry(salesOrderHeader).CurrentValues.SetValues(updatedSalesOrderHeader);
    await context.SaveChangesAsync();

    return Results.Ok(salesOrderHeader);
});


app.Run();