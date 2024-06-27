namespace Stocker_API.Sales.Domain.Model.Commands;

public record CreateSaleCommand(int ClientId, decimal TotalAmount);