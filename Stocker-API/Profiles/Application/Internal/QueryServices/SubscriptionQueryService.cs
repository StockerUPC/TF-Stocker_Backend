using Stocker_API.Profiles.Domain.Model.Entities;
using Stocker_API.Profiles.Domain.Model.Queries;
using Stocker_API.Profiles.Domain.Repositories;
using Stocker_API.Profiles.Domain.Services;

namespace Stocker_API.Profiles.Application.Internal.QueryServices;

public class SubscriptionQueryService(ISubscriptionRepository subscriptionRepository) : ISubscriptionQueryService
{
    /**
     * <summary>
     *     This method is responsible for handling GetSubscriptionByIdQuery
     * </summary>
     * <param name="query">GetSubscriptionByIdQuery>Contains the Id of the Subscription</param>
     * <returns>Subscription - The Subscription object</returns>
     */
    public async Task<Subscription?> Handle(GetSubscriptionByIdQuery query)
    {
        return await subscriptionRepository.FindByIdAsync(query.Id);
    }

    /**
     * <summary>
     *     This method is responsible for handling GetAllCategoriesQuery
     * </summary>
     * <param name="query">GetAllCategoriesQuery</param>
     * <returns>Subscription - The Subscription object</returns>
     */
    public async Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsQuery query)
    {
        return await subscriptionRepository.ListAsync();
    }
}