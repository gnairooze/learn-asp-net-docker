using SimpleWebApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var products = new List<Product>
{
    new Product { Id = 1, Name = "Product 1", Price = 10.0m },
    new Product { Id = 2, Name = "Product 2", Price = 20.0m },
    new Product { Id = 3, Name = "Product 3", Price = 30.0m }
};

var orders = new List<Order>()
{
    new Order { Id = 1, ProductId = 1, Quantity = 1, TotalPrice = 10.0m },
    new Order { Id = 2, ProductId = 2, Quantity = 2, TotalPrice = 40.0m },
    new Order { Id = 3, ProductId = 3, Quantity = 3, TotalPrice = 90.0m }
};

app.MapGet("/api/products", () => products);

app.MapGet("/api/products/{id}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

app.MapPost("/api/products", (Product product) =>
{
    product.Id = products.Count + 1;
    products.Add(product);
    return Results.Created($"/api/products/{product.Id}", product);
});

app.MapGet("/api/orders", () => orders);

app.MapGet("/api/orders/{id}", (int id) =>
{
    var order = orders.FirstOrDefault(o => o.Id == id);
    return order is not null ? Results.Ok(order) : Results.NotFound();
});

app.MapPost("/api/orders", (Order order) =>
{
    order.Id = orders.Count + 1;
    order.TotalPrice = order.Quantity * products.First(p => p.Id == order.ProductId).Price;
    orders.Add(order);
    return Results.Created($"/api/orders/{order.Id}", order);
});

app.Run();
