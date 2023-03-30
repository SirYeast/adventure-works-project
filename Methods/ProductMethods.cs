using adventure_works_project.Models;
using Microsoft.EntityFrameworkCore;

namespace adventure_works_project.Methods;

public static class ProductMethods
{
    public static async Task<IResult> CreateProduct(Product product, AdventureWorksLt2019Context db)
    {
        product.Rowguid = Guid.NewGuid();
        product.ModifiedDate = DateTime.Today;

        db.Products.Add(product);
        await db.SaveChangesAsync();
        return Results.Created($"/product/created/{product.ProductId}", product);
    }

    public static async Task<IResult> GetAllProducts(AdventureWorksLt2019Context db)
    {
        return Results.Ok(await db.Products.ToListAsync());
    }

    public static async Task<IResult> GetProductById(int id, AdventureWorksLt2019Context db)
    {
        Product? product = await db.Products.FindAsync(id);
        if (product == null)
        {
            return Results.NotFound();
        }
        else
        {
            return Results.Ok(product);
        }
    }

    public static async Task<IResult> UpdateProduct(int id, Product updatedProduct, AdventureWorksLt2019Context db)
    {
        Product? product = await db.Products.FindAsync(id);
        if (product == null)
        {
            return Results.NotFound();
        }
        else
        {
            db.Entry(product).CurrentValues.SetValues(updatedProduct);
            product.ModifiedDate = DateTime.Today; 

            await db.SaveChangesAsync();
            return Results.Ok(product);
        }
    }

    public static async Task<IResult> DeleteProduct(int id, AdventureWorksLt2019Context db)
    {
        Product? product = await db.Products.FindAsync(id);
        if (product == null)
        {
            return Results.NotFound();
        }
        else
        {
            // Update the foreign key reference in the SalesOrderDetail table
            foreach (SalesOrderDetail salesOrderDetail in product.SalesOrderDetails)
            {
                db.SalesOrderDetails.Remove(salesOrderDetail);
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return Results.Ok(product);
        }
    }

    public static async Task<IResult> GetProductDetails(int id, AdventureWorksLt2019Context db)
    {
        Product? product = await db.Products
            .Include(p => p.ProductCategory)
            .Include(p => p.ProductModel)
                .ThenInclude(m => m.ProductModelProductDescriptions)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null)
        {
            return Results.NotFound();
        }
        else
        {
            var result = new
            {
                Id = product.ProductId,
                product.Name,
                Category = product.ProductCategory?.Name,
                Model = product.ProductModel?.Name,
                Description = product.ProductModel?.ProductModelProductDescriptions
               .FirstOrDefault(d => d.Culture == "en-US")?.ProductDescription
            };
            return Results.Ok(result);
        }
    }
}
