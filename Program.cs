using adventure_works_project;
using adventure_works_project.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AdventureWorksLt2019Context>(options =>
    options.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Database=AdventureWorksLT2019;Integrated Security=True;TrustServerCertificate=True"));
var app = builder.Build();


/************    Products    **************/
app.MapPost("/Product/Create", ProductMethods.CreateProduct);
app.MapGet("/Product", ProductMethods.GetAllProducts);
app.MapGet("/Product/{productId:int}", ProductMethods.GetProductById);
app.MapPut("/Product/Update/{productId:int}", ProductMethods.UpdateProduct);
app.MapDelete("/Product/Delete/{productId:int}", ProductMethods.DeleteProduct);
app.MapGet("/Product/Details/{productId:int}", ProductMethods.GetProductDetails);

/************    SalesOrder    **************/

app.MapPost("/SalesOrderHeader/Create", SalesOrderMethods.CreateSalesOrderHeader);
app.MapGet("/SalesOrderHeader", SalesOrderMethods.GetAllSalesOrderHeaders);
app.MapGet("/SalesOrderHeader/{salesOrderId:int}", SalesOrderMethods.GetSalesOrderHeaderById);
app.MapPut("/SalesOrderHeader/Update/{salesOrderId:int}", SalesOrderMethods.UpdateSalesOrderHeader);
app.MapDelete("/SalesOrderHeader/Delete/{salesOrderId:int}", SalesOrderMethods.DeleteSalesOrderHeader);

app.Run();