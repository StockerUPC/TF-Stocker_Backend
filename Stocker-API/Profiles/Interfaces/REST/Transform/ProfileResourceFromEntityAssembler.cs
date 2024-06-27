using Stocker_API.Profiles.Domain.Model.Aggregates;
using Stocker_API.Profiles.Interfaces.REST.Resources;
using Microsoft.OpenApi.Extensions;

namespace Stocker_API.Profiles.Interfaces.REST.Transform
{
    public static class ProfileResourceFromEntityAssembler
    {
        public static ProfileResource ToResourceFromEntity(Profile profile)
        {
            return new ProfileResource(
                profile.Id,
                profile.FullName,
                profile.EmailAddress,
                profile.StreetAddress,
                SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(profile.Subscription)
            );
        }
    }
}