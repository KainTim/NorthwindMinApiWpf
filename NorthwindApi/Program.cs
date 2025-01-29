using Microsoft.EntityFrameworkCore;

using NorthwindApi.Maps;
using NorthwindApi.Services;

using NorthwindDbLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("NorthwindContext");
Console.WriteLine(connectionString);
builder.Services.AddDbContext<NorthwindContext>(options => options.UseSqlite(connectionString));
builder.Services.AddScoped<DbService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapGet("employees", (DbService service) => service.getEmployees());
app.MapGet("customers", (DbService service) => service.getCustomers());
app.MapGet("products", (DbService service) => service.getProducts());
app.MapOrders();
app.MapOrderDetails();
app.Run();
