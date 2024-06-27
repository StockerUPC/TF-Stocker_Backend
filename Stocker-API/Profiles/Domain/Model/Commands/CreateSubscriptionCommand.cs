namespace Stocker_API.Profiles.Domain.Model.Commands;

public record CreateSubscriptionCommand(string Name, decimal MonthlyPrice);