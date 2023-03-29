using adventure_works_project.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AdventureWorksLt2019Context>();
var app = builder.Build();

app.MapGet("/customers/{id?}", CustomerMethods.GetCustomers);
app.MapGet("/customers/details/{id}", CustomerMethods.Details);
app.MapPost("/customers/create", CustomerMethods.Create);
app.MapPut("/customers/update/{id}", CustomerMethods.Update);
app.MapDelete("/customers/delete/{id}", CustomerMethods.Delete);
app.MapPost("/customers/addtoaddress", CustomerMethods.AddToAddress);

app.Run();