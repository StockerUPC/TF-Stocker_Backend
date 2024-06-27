using Stocker_API.Purchases.Domain.Model.Entities;
using Stocker_API.Purchases.Domain.Model.Queries;

namespace Stocker_API.Purchases.Domain.Services;

public interface ISupplierQueryService
{
    Task<Supplier?> Handle(GetSupplierByIdQuery query);
    Task<IEnumerable<Supplier>> Handle(GetAllSuppliersQuery query);
}