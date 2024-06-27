namespace Stocker_API.Purchases.Interfaces.REST.Resources;

public record CreatePurchaseDetailResource(int PurchaseId, int ProductId, decimal PurchasePrice, decimal SalePrice, int Quantity, decimal Total);
