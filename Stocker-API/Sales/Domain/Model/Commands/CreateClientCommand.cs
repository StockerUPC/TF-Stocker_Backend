namespace Stocker_API.Sales.Domain.Model.Commands;

public record CreateClientCommand(string Name, string Number, string Email);