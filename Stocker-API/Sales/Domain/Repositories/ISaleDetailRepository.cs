using Stocker_API.Sales.Domain.Model.Aggregates;
using Stocker_API.Shared.Domain.Repositories;

namespace Stocker_API.Sales.Domain.Repositories;

public interface ISaleDetailRepository : IBaseRepository<SaleDetail>
{
    Task<IEnumerable<SaleDetail>> FindBySaleIdAsync(int saleId);
}