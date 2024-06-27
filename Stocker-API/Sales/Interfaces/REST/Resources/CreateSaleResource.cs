namespace Stocker_API.Sales.Interfaces.REST.Resources;

public record CreateSaleResource(int ClientId, decimal TotalAmount);