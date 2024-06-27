using Stocker_API.Sales.Domain.Model.Aggregates;
using Stocker_API.Shared.Domain.Repositories;

namespace Stocker_API.Sales.Domain.Repositories;

public interface ISaleRepository : IBaseRepository<Sale>
{
    Task<IEnumerable<Sale>> FindByClientIdAsync(int clientId);
}