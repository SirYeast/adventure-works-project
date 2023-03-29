using adventure_works_project.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AdventureWorksLt2019Context>(options =>
    options.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Database=AdventureWorksLT2019;Integrated Security=True;TrustServerCertificate=True"));
var app = builder.Build();

/****** Products *******/

// 201 Created

app.MapPost("/Product/Create", async (Product product, AdventureWorksLt2019Context context) =>
{
    context.Products.Add(product);
    await context.SaveChangesAsync();
    return Results.Created($"/Product/{product.ProductId}", product);
});

// Get all Products 200 OK
app.MapGet("/Product", async (AdventureWorksLt2019Context context) =>
{
    List<Product> products = await context.Products.ToListAsync();
    return Results.Ok(products);
});

// Get Products by Id 200 OK
app.MapGet("/Product/{productId:int}", async (int productId, AdventureWorksLt2019Context context) =>
{
    Product products = await context.Products.FindAsync(productId);
    if (products == null)
    {
        return Results.NotFound();
    } else
    {
        return Results.Ok(products);
    }
});

// Update Product by primary key (ProductId)
app.MapPut("/Product/Update/{productId:int}", async (int productId, Product updatedProduct, AdventureWorksLt2019Context context) =>
{
    Product product = await context.Products.FindAsync(productId);
    if (product == null)
    {
        return Results.NotFound();
    }
    else
    {
        context.Entry(product).CurrentValues.SetValues(updatedProduct);
        await context.SaveChangesAsync();

        return Results.Ok(product);
    }

});


// Delete Product by primary key (ProductId)
app.MapDelete("/Product/Delete/{productId:int}", async (int productId, AdventureWorksLt2019Context context) =>
{
    Product product = await context.Products.FindAsync(productId);
    if (product == null)
    {
        return Results.NotFound();

    } else
    {
        // Update the foreign key reference in the SalesOrderDetail table
        foreach (SalesOrderDetail salesOrderDetail in product.SalesOrderDetails)
        {
            context.SalesOrderDetails.Remove(salesOrderDetail);
        }

        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return Results.Ok(product);

    }

});

app.MapGet("/Product/Details/{productId:int}", async (int productId, AdventureWorksLt2019Context context) =>
{
    Product product = await context.Products
        .Include(p => p.ProductCategory)
        .Include(p => p.ProductModel)
            .ThenInclude(m => m.ProductModelProductDescriptions)
        .FirstOrDefaultAsync(p => p.ProductId == productId);


    if (product == null) 
    {
        return Results.NotFound();
    } 
    else
    {
        var result = new
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Category = product.ProductCategory?.Name,
            Model = product.ProductModel?.Name,
            Description = product.ProductModel?.ProductModelProductDescriptions
           .FirstOrDefault(d => d.Culture == "en-US")?.ProductDescription
        };
        return Results.Ok(result);
    }

   
});


/****** SalesOrderHeader *******/

// Create a SalesOrderHeader
// 201 Created
app.MapPost("/SalesOrderHeader/Create", async (SalesOrderHeader salesOrderHeader, AdventureWorksLt2019Context context) =>
{
    context.SalesOrderHeaders.Add(salesOrderHeader);
    await context.SaveChangesAsync();
    return Results.Created($"/SalesOrderHeader/{salesOrderHeader.SalesOrderId}", salesOrderHeader);
});

// Get all SalesOrderHeaders
app.MapGet("/SalesOrderHeader", (AdventureWorksLt2019Context context) =>
{
    List<SalesOrderHeader> salesOrderHeaders = context.SalesOrderHeaders.ToList();
    return Results.Ok(salesOrderHeaders);
});

// Get SalesOrderHeaders by Id
app.MapGet("/SalesOrderHeader/{salesOrderId:int}", async (int salesOrderId, AdventureWorksLt2019Context context) =>
{
    SalesOrderHeader salesOrderHeaders = await context.SalesOrderHeaders.FindAsync(salesOrderId);
    if (salesOrderHeaders == null)
    {
        return Results.NotFound();
    } else
    {
        return Results.Ok(salesOrderHeaders);
    }
});


// Update SalesOrderHeader by primary key (SalesOrderId)
app.MapPut("/SalesOrderHeader/Update/{salesOrderId:int}", async (int salesOrderId, SalesOrderHeader updatedSalesOrderHeader, AdventureWorksLt2019Context context) =>
{
    SalesOrderHeader salesOrderHeader = await context.SalesOrderHeaders.FindAsync(salesOrderId);
    if (salesOrderHeader == null)
    {
        return Results.NotFound();
    }
    else
    {
        context.Entry(salesOrderHeader).CurrentValues.SetValues(updatedSalesOrderHeader);
        await context.SaveChangesAsync();
        return Results.Ok(salesOrderHeader);
    }
});


// Delete Product by primary key (ProductId)
app.MapDelete("/SalesOrderHeader/Delete/{salesOrderId:int}", async (int salesOrderId, AdventureWorksLt2019Context context) =>
{
    SalesOrderHeader salesOrderHeader = await context.SalesOrderHeaders.FindAsync(salesOrderId);
    if (salesOrderHeader == null)
    {
        return Results.NotFound();
    } 
    else
    {
        context.SalesOrderHeaders.Remove(salesOrderHeader);
        await context.SaveChangesAsync();

        return Results.Ok(salesOrderHeader);
    }

});

app.Run();