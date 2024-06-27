using Stocker_API.Profiles.Domain.Model.Aggregates;
using Stocker_API.Profiles.Domain.Model.ValueObjects;
using Stocker_API.Profiles.Domain.Repositories;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Stocker_API.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class ProfileRepository(AppDbContext context) : BaseRepository<Profile>(context), IProfileRepository
{
    public Task<Profile?> FindProfileByEmailAsync(EmailAddress email)
    {
        return Context.Set<Profile>().Where(p => p.Email == email).FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<Profile?>> ListBySubscriptionIdAsync(int subscriptionId)
    {
        return await Context.Set<Profile>()
            .Where(p => p.SubscriptionId == subscriptionId)
            .ToListAsync();
    }
}