using Stocker_API.Profiles.Domain.Model.Entities;

namespace Stocker_API.Profiles.Interfaces.REST.Resources;

public record ProfileResource(int Id, string FullName, string Email, string StreetAddress, SubscriptionResource Subscription);