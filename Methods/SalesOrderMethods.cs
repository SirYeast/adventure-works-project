using adventure_works_project.Models;
using Microsoft.EntityFrameworkCore;

namespace adventure_works_project.Methods;

public static class SalesOrderMethods
{
    public static async Task<IResult> CreateSalesOrderHeader(SalesOrderHeader salesOrderHeader, AdventureWorksLt2019Context db)
    {
        salesOrderHeader.Rowguid = Guid.NewGuid();
        salesOrderHeader.ModifiedDate = DateTime.Today;

        db.SalesOrderHeaders.Add(salesOrderHeader);
        await db.SaveChangesAsync();
        return Results.Created($"/salesorderheader/created/{salesOrderHeader.SalesOrderId}", salesOrderHeader);
    }

    public static async Task<IResult> GetAllSalesOrderHeaders(AdventureWorksLt2019Context db)
    {
        return Results.Ok(await db.SalesOrderHeaders.ToListAsync());
    }

    public static async Task<IResult> GetSalesOrderHeaderById(int id, AdventureWorksLt2019Context db)
    {
        SalesOrderHeader? salesOrderHeader = await db.SalesOrderHeaders.FindAsync(id);
        if (salesOrderHeader == null)
        {
            return Results.NotFound();
        }
        else
        {
            return Results.Ok(salesOrderHeader);
        }
    }

    public static async Task<IResult> UpdateSalesOrderHeader(int id, SalesOrderHeader updatedSalesOrderHeader, AdventureWorksLt2019Context db)
    {
        SalesOrderHeader? salesOrderHeader = await db.SalesOrderHeaders.FindAsync(id);
        if (salesOrderHeader == null)
        {
            return Results.NotFound();
        }
        else
        {
            db.Entry(salesOrderHeader).CurrentValues.SetValues(updatedSalesOrderHeader);
            salesOrderHeader.ModifiedDate = DateTime.Today;

            await db.SaveChangesAsync();
            return Results.Ok(salesOrderHeader);
        }
    }

    public static async Task<IResult> DeleteSalesOrderHeader(int id, AdventureWorksLt2019Context db)
    {
        SalesOrderHeader? salesOrderHeader = await db.SalesOrderHeaders.FindAsync(id);
        if (salesOrderHeader == null)
        {
            return Results.NotFound();
        }
        else
        {
            db.SalesOrderHeaders.Remove(salesOrderHeader);
            await db.SaveChangesAsync();

            return Results.Ok(salesOrderHeader);
        }
    }
}