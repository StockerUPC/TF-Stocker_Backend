namespace Stocker_API.Purchases.Domain.Model.Commands;

public record CreatePurchaseDetailCommand(int PurchaseId, int ProductId,decimal PurchasePrice, decimal SalePrice, int Quantity, decimal Total);