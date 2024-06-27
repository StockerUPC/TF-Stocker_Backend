namespace Stocker_API.Sales.Interfaces.REST.Resources;

public record SaleResource(int Id, ClientResource Client, decimal TotalAmount);