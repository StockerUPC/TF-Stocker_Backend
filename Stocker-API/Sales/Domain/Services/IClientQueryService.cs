using Stocker_API.Sales.Domain.Model.Entities;
using Stocker_API.Sales.Domain.Model.Queries;

namespace Stocker_API.Sales.Domain.Services;

public interface IClientQueryService
{
    Task<Client?> Handle(GetClientByIdQuery query);
    Task<IEnumerable<Client>> Handle(GetAllClientsQuery query);
}