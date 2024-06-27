using Stocker_API.Profiles.Domain.Model.Aggregates;
using Stocker_API.Profiles.Domain.Model.Commands;
using Stocker_API.Profiles.Domain.Repositories;
using Stocker_API.Profiles.Domain.Services;
using Stocker_API.Shared.Domain.Repositories;

namespace Stocker_API.Profiles.Application.Internal.CommandServices;

public class ProfileCommandService(IProfileRepository profileRepository, IUnitOfWork unitOfWork) : IProfileCommandService
{
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        var profile = new Profile(command);
        try
        {
            await profileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        } catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the profile: {e.Message}");
            return null;
        }
    }
    
    public async Task<Profile?> Delete(Profile profile)
    {
        try
        {
            profileRepository.Remove(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        } catch (Exception e)
        {
            Console.WriteLine($"An error occurred while deleting the profile: {e.Message}");
            return null;
        }
    }
    
    public async Task<Profile?> Update(Profile profile)
    {
        try
        {
            profileRepository.Update(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        } catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the profile: {e.Message}");
            return null;
        }
    }
}