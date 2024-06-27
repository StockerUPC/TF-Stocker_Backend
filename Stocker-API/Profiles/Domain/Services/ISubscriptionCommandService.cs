using Stocker_API.Profiles.Domain.Model.Commands;
using Stocker_API.Profiles.Domain.Model.Entities;

namespace Stocker_API.Profiles.Domain.Services;

public interface ISubscriptionCommandService
{
    public Task<Subscription?> Handle(CreateSubscriptionCommand command);
    public Task<Subscription?> Delete(Subscription subscription);
    public Task<Subscription?> Update(Subscription subscription);
}