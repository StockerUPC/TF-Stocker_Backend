namespace Stocker_API.Purchases.Interfaces.REST.Resources;

public record PurchaseResource(int Id, SupplierResource Supplier, decimal TotalAmount);