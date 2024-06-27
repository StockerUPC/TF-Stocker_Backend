using System.Net.Mime;
using Stocker_API.Sales.Domain.Model.Queries;
using Stocker_API.Sales.Domain.Services;
using Stocker_API.Sales.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace Stocker_API.Sales.Interfaces.REST;

[ApiController]
[Route("/api/v1/clients/{clientId}/sales")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Clients")]
public class ClientSalesController(ISaleQueryService saleQueryService) : ControllerBase
{
    
    [HttpGet]
    public async Task<IActionResult> GetSalesByClientId([FromRoute] int clientId)
    {
        var getAllSalesByClientIdQuery = new GetAllSalesByClientIdQuery(clientId);
        var sales = await saleQueryService.Handle(getAllSalesByClientIdQuery);
        var resources = sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}