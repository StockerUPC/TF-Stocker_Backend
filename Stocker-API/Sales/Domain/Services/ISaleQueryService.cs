using Stocker_API.Sales.Domain.Model.Aggregates;
using Stocker_API.Sales.Domain.Model.Queries;

namespace Stocker_API.Sales.Domain.Services;

public interface ISaleQueryService
{
    Task<Sale?> Handle(GetSaleByIdQuery query);
    Task<IEnumerable<Sale>> Handle(GetAllSalesQuery query);
    Task<IEnumerable<Sale>> Handle(GetAllSalesByClientIdQuery query);
}