namespace Stocker_API.Purchases.Interfaces.REST.Resources;

public record CreatePurchaseResource(int SupplierId, decimal TotalAmount);