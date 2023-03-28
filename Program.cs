using adventure_works_project.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AdventureWorksLt2019Context>();
var app = builder.Build();

app.Run();