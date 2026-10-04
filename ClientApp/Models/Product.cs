namespace ClientApp.Models;

public sealed record Product(int Id, string Name, decimal Price, int Stock, ProductCategory Category);

public sealed record ProductCategory(int Id, string Name);