using BakeryAPI.Data;
using BakeryAPI.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<BakeryContext>(options =>
    options.UseSqlite("Data Source=bakery.db"));

var app = builder.Build();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BakeryContext>();

    context.Database.EnsureCreated();

    if (!context.Customers.Any())
    {
        var customer = new Customer
        {
            StoreName = "ICA Maxi",
            Phone = "123456",
            Email = "ica@butik.se",
            ContactPerson = "Anna",
            DeliveryAddress = "Stockholm",
            InvoiceAddress = "Stockholm"
        };

        var product = new Product
        {
            Name = "Semla",
            PricePerPiece = 25,
            Weight = 150,
            PackageSize = 2,
            BestBefore = DateTime.Now.AddDays(3),
            ManufactureDate = DateTime.Now
        };

        context.Customers.Add(customer);
        context.Products.Add(product);
        context.SaveChanges();

        context.Orders.Add(new Order
        {
            OrderDate = DateTime.Now,
            OrderNumber = "ORD001",
            CustomerId = customer.Id,
            ProductId = product.Id,
            Quantity = 3,
            Price = product.PricePerPiece,
            TotalPrice = product.PricePerPiece * 3
        });

        context.SaveChanges();
    }
}

app.Run();