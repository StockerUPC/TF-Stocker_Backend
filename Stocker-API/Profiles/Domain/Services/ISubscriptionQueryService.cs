using Stocker_API.Profiles.Domain.Model.Entities;
using Stocker_API.Profiles.Domain.Model.Queries;

namespace Stocker_API.Profiles.Domain.Services;

public interface ISubscriptionQueryService
{
    Task<Subscription?> Handle(GetSubscriptionByIdQuery query);
    Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsQuery query);
    
    
}