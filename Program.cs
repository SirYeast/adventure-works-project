using adventure_works_project;
using adventure_works_project.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AdventureWorksLt2019Context>();
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

/************    Addresses    **************/
app.MapPost(@"/address/create", AddressMethods.Create);
app.MapGet("/address/{id?}", AddressMethods.Read);
app.MapPut("/address/update/{id}", AddressMethods.Update);
app.MapDelete("/address/delete/{id}", AddressMethods.Delete);
app.MapGet("/address/details/{id}", AddressMethods.Details);

/************    Customers    **************/
app.MapGet("/customers/{id?}", CustomerMethods.GetCustomers);
app.MapGet("/customers/details/{id}", CustomerMethods.Details);
app.MapPost("/customers/create", CustomerMethods.Create);
app.MapPut("/customers/update/{id}", CustomerMethods.Update);
app.MapDelete("/customers/delete/{id}", CustomerMethods.Delete);
app.MapPost("/customers/addtoaddress", CustomerMethods.AddToAddress);

app.Run();