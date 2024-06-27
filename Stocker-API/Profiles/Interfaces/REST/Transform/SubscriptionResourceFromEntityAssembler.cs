using Stocker_API.Profiles.Domain.Model.Entities;
using Stocker_API.Profiles.Interfaces.REST.Resources;

namespace Stocker_API.Profiles.Interfaces.REST.Transform;

public static class SubscriptionResourceFromEntityAssembler
{
    public static SubscriptionResource ToResourceFromEntity(Subscription subscription)
    {
        return new SubscriptionResource(
            subscription.Id,
            subscription.Name,
            subscription.MonthlyPrice
        );
    }
}