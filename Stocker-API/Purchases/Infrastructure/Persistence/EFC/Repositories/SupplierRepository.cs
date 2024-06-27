using Stocker_API.Purchases.Domain.Model.Entities;
using Stocker_API.Purchases.Domain.Repositories;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Stocker_API.Purchases.Infrastructure.Persistence.EFC.Repositories;

public class SupplierRepository(AppDbContext context) : BaseRepository<Supplier>(context), ISupplierRepository;