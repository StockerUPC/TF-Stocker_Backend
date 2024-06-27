using System.Net.Mime;
using Stocker_API.Purchases.Domain.Model.Queries;
using Stocker_API.Purchases.Domain.Services;
using Stocker_API.Purchases.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace Stocker_API.Purchases.Interfaces.REST;

[ApiController]
[Route("/api/v1/purchases/{purchased}/purchaseDetails")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Purchases")]
public class PurchasePurchaseDetailsController(IPurchaseDetailQueryService purchaseDetailQueryService) : ControllerBase
{
    
    [HttpGet]
    public async Task<IActionResult> GetPurchaseDetailsByPurchaseId([FromRoute] int purchaseDetailId)
    {
        var getAllPurchaseDetailsByPurchaseIdQuery = new GetAllPurchaseDetailsByPurchaseIdQuery(purchaseDetailId);
        var purchaseDetails = await purchaseDetailQueryService.Handle(getAllPurchaseDetailsByPurchaseIdQuery);
        var resources = purchaseDetails.Select(PurchaseDetailResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}