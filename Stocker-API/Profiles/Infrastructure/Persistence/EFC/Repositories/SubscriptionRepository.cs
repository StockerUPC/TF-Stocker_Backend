using Stocker_API.Profiles.Domain.Model.Entities;
using Stocker_API.Profiles.Domain.Repositories;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Stocker_API.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class SubscriptionRepository(AppDbContext context) : BaseRepository<Subscription>(context), ISubscriptionRepository;