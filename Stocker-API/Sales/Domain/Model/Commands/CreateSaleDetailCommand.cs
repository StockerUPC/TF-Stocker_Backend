namespace Stocker_API.Sales.Domain.Model.Commands;

public record CreateSaleDetailCommand(int SaleId, int ProductId, decimal SalePrice, int Quantity, decimal Subtotal);