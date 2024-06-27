using Stocker_API.Profiles.Domain.Model.Aggregates;
using Stocker_API.Profiles.Domain.Model.ValueObjects;
using Stocker_API.Shared.Domain.Repositories;

namespace Stocker_API.Profiles.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Profile>
{
    Task<Profile?> FindProfileByEmailAsync(EmailAddress email);
    Task<IEnumerable<Profile?>> ListBySubscriptionIdAsync(int subscriptionId);
}