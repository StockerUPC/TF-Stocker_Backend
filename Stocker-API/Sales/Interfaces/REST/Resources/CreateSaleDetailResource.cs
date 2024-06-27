namespace Stocker_API.Sales.Interfaces.REST.Resources;

public record CreateSaleDetailResource(int SaleId, int ProductId, decimal SalePrice, int Quantity, decimal Subtotal);
