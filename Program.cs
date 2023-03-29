using adventure_works_project.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdventureWorksLt2019Context>();
var app = builder.Build();

//Address Create
app.MapPost(@"/address/create", AddressMethods.Create);
//Address Read
app.MapGet("/address/{id?}", AddressMethods.Read);
//Address Update
app.MapPut("/address/update/{id}", AddressMethods.Update);
//Address Delete
app.MapDelete("/address/delete/{id}", AddressMethods.Delete);
//Address Details
app.MapGet("/address/details/{id}", AddressMethods.Details);

app.MapGet("/customers/{id?}", CustomerMethods.GetCustomers);
app.MapGet("/customers/details/{id}", CustomerMethods.Details);
app.MapPost("/customers/create", CustomerMethods.Create);
app.MapPut("/customers/update/{id}", CustomerMethods.Update);
app.MapDelete("/customers/delete/{id}", CustomerMethods.Delete);
app.MapPost("/customers/addtoaddress", CustomerMethods.AddToAddress);

app.Run();