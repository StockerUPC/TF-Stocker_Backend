namespace Stocker_API.Purchases.Domain.Model.Commands;

public record CreatePurchaseCommand(int SupplierId, decimal TotalAmount);