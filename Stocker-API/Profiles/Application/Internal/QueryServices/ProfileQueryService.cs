using Stocker_API.Profiles.Domain.Model.Aggregates;
using Stocker_API.Profiles.Domain.Model.Queries;
using Stocker_API.Profiles.Domain.Repositories;
using Stocker_API.Profiles.Domain.Services;

namespace Stocker_API.Profiles.Application.Internal.QueryServices;

public class ProfileQueryService(IProfileRepository profileRepository) : IProfileQueryService
{
    public async Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query)
    {
        return await profileRepository.ListAsync();
    }

    public async Task<Profile?> Handle(GetProfileByEmailQuery query)
    {
        return await profileRepository.FindProfileByEmailAsync(query.Email);
    }

    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.FindByIdAsync(query.ProfileId);
    }
    public async Task<IEnumerable<Profile?>> Handle(GetAllProfilesBySubscriptionIdQuery query)
    {
        return await profileRepository.ListBySubscriptionIdAsync(query.SubscriptionId);
    }
}