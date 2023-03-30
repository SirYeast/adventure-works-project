using adventure_works_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace adventure_works_project
{
    public class ProductMethods
    {
        public static async Task<IResult> CreateProduct(Product product, AdventureWorksLt2019Context db)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/Product/{product.ProductId}", product);
        }

        public static async Task<IResult> GetAllProducts(AdventureWorksLt2019Context db)
        {
            List<Product> products = await db.Products.ToListAsync();
            return Results.Ok(products);
        }

        public static async Task<IResult> GetProductById(int productId, AdventureWorksLt2019Context db)
        {
            Product product = await db.Products.FindAsync(productId);
            if (product == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(product);
            }
        }

        public static async Task<IResult> UpdateProduct(int productId, Product updatedProduct, AdventureWorksLt2019Context db)
        {
            Product product = await db.Products.FindAsync(productId);
            if (product == null)
            {
                return Results.NotFound();
            }
            else
            {
                db.Entry(product).CurrentValues.SetValues(updatedProduct);
                await db.SaveChangesAsync();

                return Results.Ok(product);
            }
        }

        public static async Task<IResult> DeleteProduct(int productId, AdventureWorksLt2019Context db)
        {
            Product product = await db.Products.FindAsync(productId);
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

        public static async Task<IResult> GetProductDetails(int productId, AdventureWorksLt2019Context db)
        {
            Product product = await db.Products
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
                    Id = product.ProductId,
                    Name = product.Name,
                    Category = product.ProductCategory?.Name,
                    Model = product.ProductModel?.Name,
                    Description = product.ProductModel?.ProductModelProductDescriptions
                   .FirstOrDefault(d => d.Culture == "en-US")?.ProductDescription
                };
                return Results.Ok(result);
            }
        }
    }
}
