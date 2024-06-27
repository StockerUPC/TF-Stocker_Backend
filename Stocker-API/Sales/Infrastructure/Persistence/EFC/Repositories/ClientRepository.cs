using Stocker_API.Sales.Domain.Model.Entities;
using Stocker_API.Sales.Domain.Repositories;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Stocker_API.Sales.Infrastructure.Persistence.EFC.Repositories;

public class ClientRepository(AppDbContext context) : BaseRepository<Client>(context), IClientRepository;