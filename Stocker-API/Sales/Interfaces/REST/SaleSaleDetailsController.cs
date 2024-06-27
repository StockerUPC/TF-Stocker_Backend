using System.Net.Mime;
using Stocker_API.Sales.Domain.Model.Queries;
using Stocker_API.Sales.Domain.Services;
using Stocker_API.Sales.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Stocker_API.Sales.Interfaces.REST;

[ApiController]
[Route("/api/v1/sales/{saleId}/saleDetails")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Sales")]
public class SaleSaleDetailsController(ISaleDetailQueryService saleDetailQueryService) : ControllerBase
{
    
    [HttpGet]
    public async Task<IActionResult> GetSaleDetailsBySaleId([FromRoute] int saleDetailId)
    {
        var getAllSaleDetailsBySaleIdQuery = new GetAllSaleDetailBySaleIdQuery(saleDetailId);
        var saleDetails = await saleDetailQueryService.Handle(getAllSaleDetailsBySaleIdQuery);
        var resources = saleDetails.Select(SaleDetailResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}