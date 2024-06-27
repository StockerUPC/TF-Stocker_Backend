using Stocker_API.Profiles.Domain.Model.Aggregates;
using Stocker_API.Profiles.Domain.Model.Commands;

namespace Stocker_API.Profiles.Domain.Services;

public interface IProfileCommandService
{
    Task<Profile?> Handle(CreateProfileCommand command);
    Task<Profile?> Delete(Profile profile);
    Task<Profile?> Update(Profile profile);
}