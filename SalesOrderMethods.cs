using adventure_works_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace adventure_works_project
{
    public class SalesOrderMethods
    {
        public static async Task<IResult> CreateSalesOrderHeader(SalesOrderHeader salesOrderHeader, AdventureWorksLt2019Context db)
        {
            db.SalesOrderHeaders.Add(salesOrderHeader);
            await db.SaveChangesAsync();
            return Results.Created($"/SalesOrderHeader/{salesOrderHeader.SalesOrderId}", salesOrderHeader);
        }

        public static async Task<IResult> GetAllSalesOrderHeaders(AdventureWorksLt2019Context db)
        {
            List<SalesOrderHeader> salesOrderHeaders = db.SalesOrderHeaders.ToList();
            return Results.Ok(salesOrderHeaders);
        }

        public static async Task<IResult> GetSalesOrderHeaderById(int salesOrderId, AdventureWorksLt2019Context db)
        {
            SalesOrderHeader salesOrderHeader = await db.SalesOrderHeaders.FindAsync(salesOrderId);
            if (salesOrderHeader == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(salesOrderHeader);
            }
        }

        public static async Task<IResult> UpdateSalesOrderHeader(int salesOrderId, SalesOrderHeader updatedSalesOrderHeader, AdventureWorksLt2019Context db)
        {
            SalesOrderHeader salesOrderHeader = await db.SalesOrderHeaders.FindAsync(salesOrderId);
            if (salesOrderHeader == null)
            {
                return Results.NotFound();
            }
            else
            {
                db.Entry(salesOrderHeader).CurrentValues.SetValues(updatedSalesOrderHeader);
                await db.SaveChangesAsync();
                return Results.Ok(salesOrderHeader);
            }
        }

        public static async Task<IResult> DeleteSalesOrderHeader(int salesOrderId, AdventureWorksLt2019Context db)
        {
            SalesOrderHeader salesOrderHeader = await db.SalesOrderHeaders.FindAsync(salesOrderId);
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
}
