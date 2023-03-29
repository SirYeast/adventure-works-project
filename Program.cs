using adventure_works_project.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AdventureWorksLt2019Context>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Address Create
app.MapPost(@"/address/create", AddressMethods.Create);
//Address Read
app.MapGet("/address/{id?}", AddressMethods.Read);
//Address Update
app.MapPut("/address/update/{id}", AddressMethods.Update);
//Address Delete
app.MapDelete("/address/delete/{id}", AddressMethods.Delete);

app.Run();