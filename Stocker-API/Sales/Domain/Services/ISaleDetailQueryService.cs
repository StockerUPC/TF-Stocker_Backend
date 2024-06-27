using Stocker_API.Sales.Domain.Model.Aggregates;
using Stocker_API.Sales.Domain.Model.Queries;

namespace Stocker_API.Sales.Domain.Services;

public interface ISaleDetailQueryService
{
    Task<SaleDetail?> Handle(GetSaleDetailByIdQuery query);
    Task<IEnumerable<SaleDetail>> Handle(GetAllSaleDetailsQuery query);
    Task<IEnumerable<SaleDetail>> Handle(GetAllSaleDetailBySaleIdQuery query);
}