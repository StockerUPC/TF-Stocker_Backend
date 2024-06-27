namespace Stocker_API.Purchases.Domain.Model.Commands;

public record CreateSupplierCommand(string Name, string Number, string Email);