using Stocker_API.Profiles.Domain.Model.ValueObjects;
using Stocker_API.Profiles.Interfaces.ACL;
using Stocker_API.Inventory.Domain.Model.ValueObjects;


namespace Stocker_API.Inventory.Application.Internal.OutboundServices.ACL;

public class ExternalProfileService(IProfilesContextFacade profilesContextFacade)
{

    public async Task<ProfileId?> FetchProfileIdByEmail(string email)
    {
        var profileId = await profilesContextFacade.FetchProfileIdByEmail(email);
        if (profileId == 0) return await Task.FromResult<ProfileId?>(null);
        return new ProfileId(profileId);
    }

    public async Task<ProfileId?> CreateProfile(string firstName, string lastName, string email, string street,
        string number, string city, string postalCode, string country, int subscriptionId)
    {
        var profileId = await profilesContextFacade.CreateProfile(firstName, lastName, email, street, number, city,
            postalCode, country, subscriptionId);
        if (profileId == 0) return await Task.FromResult<ProfileId?>(null);
        return new ProfileId(profileId);
    }
    
}