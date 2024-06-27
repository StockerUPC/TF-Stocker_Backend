using Stocker_API.Profiles.Domain.Model.ValueObjects;

namespace Stocker_API.Profiles.Domain.Model.Queries;

public record GetProfileByEmailQuery(EmailAddress Email);