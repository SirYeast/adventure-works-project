using adventure_works_project.Methods;
using adventure_works_project.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AdventureWorksLt2019Context>();
WebApplication app = builder.Build();

/************    Products    **************/
app.MapPost("/product/create", ProductMethods.CreateProduct);
app.MapGet("/product", ProductMethods.GetAllProducts);
app.MapGet("/product/{id}", ProductMethods.GetProductById);
app.MapPut("/product/update/{id}", ProductMethods.UpdateProduct);
app.MapDelete("/product/delete/{id}", ProductMethods.DeleteProduct);
app.MapGet("/product/details/{id}", ProductMethods.GetProductDetails);

/************    SalesOrder    **************/
app.MapPost("/salesorderheader/create", SalesOrderMethods.CreateSalesOrderHeader);
app.MapGet("/salesorderheader", SalesOrderMethods.GetAllSalesOrderHeaders);
app.MapGet("/salesorderheader/{id}", SalesOrderMethods.GetSalesOrderHeaderById);
app.MapPut("/salesorderheader/update/{id}", SalesOrderMethods.UpdateSalesOrderHeader);
app.MapDelete("/salesorderheader/delete/{id}", SalesOrderMethods.DeleteSalesOrderHeader);

/************    Addresses    **************/
app.MapPost("/address/create", AddressMethods.Create);
app.MapGet("/address/{id?}", AddressMethods.GetAddresses);
app.MapPut("/address/update/{id}", AddressMethods.Update);
app.MapDelete("/address/delete/{id}", AddressMethods.Delete);
app.MapGet("/address/details/{id}", AddressMethods.Details);

/************    Customers    **************/
app.MapGet("/customer/{id?}", CustomerMethods.GetCustomers);
app.MapGet("/customer/details/{id}", CustomerMethods.Details);
app.MapPost("/customer/create", CustomerMethods.Create);
app.MapPut("/customer/update/{id}", CustomerMethods.Update);
app.MapDelete("/customer/delete/{id}", CustomerMethods.Delete);
app.MapPost("/customer/addtoaddress", CustomerMethods.AddToAddress);

app.Run();