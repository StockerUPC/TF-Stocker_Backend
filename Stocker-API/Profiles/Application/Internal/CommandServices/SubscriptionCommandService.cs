using Stocker_API.Profiles.Domain.Model.Commands;
using Stocker_API.Profiles.Domain.Model.Entities;
using Stocker_API.Profiles.Domain.Repositories;
using Stocker_API.Profiles.Domain.Services;
using Stocker_API.Shared.Domain.Repositories;

namespace Stocker_API.Profiles.Application.Internal.CommandServices;

public class SubscriptionCommandService(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
    : ISubscriptionCommandService
{
    public async Task<Subscription?> Handle(CreateSubscriptionCommand command)
    {
        var subscription = new Subscription(command.Name, command.MonthlyPrice);
        await subscriptionRepository.AddAsync(subscription);
        await unitOfWork.CompleteAsync();
        return subscription;
    }

    public async Task<Subscription?> Delete(Subscription subscription)
    {
        subscriptionRepository.Remove(subscription);
        await unitOfWork.CompleteAsync();
        return subscription;
    }

    public async Task<Subscription?> Update(Subscription subscription)
    {
        subscriptionRepository.Update(subscription);
        await unitOfWork.CompleteAsync();
        return subscription;
    }
    
}