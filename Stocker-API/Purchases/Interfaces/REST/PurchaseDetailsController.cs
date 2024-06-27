using Stocker_API.Purchases.Domain.Model.Queries;
using Stocker_API.Purchases.Domain.Services;
using Stocker_API.Purchases.Interfaces.REST.Resources;
using Stocker_API.Purchases.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Stocker_API.Purchases.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class PurchasesDetailsController(
    IPurchaseDetailCommandService purchaseDetailCommandService,
    IPurchaseDetailQueryService purchaseDetailQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePurchaseDetailDetail([FromBody] CreatePurchaseDetailResource createPurchaseDetailResource)
    {
        var createPurchaseDetailCommand =
            CreatePurchaseDetailCommandFromResourceAssembler.ToCommandFromResource(createPurchaseDetailResource);
        var purchaseDetail = await purchaseDetailCommandService.Handle(createPurchaseDetailCommand);
        if (purchaseDetail is null) return BadRequest();
        var resource = PurchaseDetailResourceFromEntityAssembler.ToResourceFromEntity(purchaseDetail);
        return CreatedAtAction(nameof(GetPurchaseDetailById), new { purchaseDetailId = resource.Id }, resource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPurchaseDetails()
    {
        var getAllPurchaseDetailsQuery = new GetAllPurchaseDetailsQuery();
        var purchaseDetails = await purchaseDetailQueryService.Handle(getAllPurchaseDetailsQuery);
        var resources = purchaseDetails.Select(PurchaseDetailResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{purchaseDetailId}")]
    public async Task<IActionResult> GetPurchaseDetailById([FromRoute] int purchaseDetailId)
    {
        var purchaseDetail = await purchaseDetailQueryService.Handle(new GetPurchaseDetailByIdQuery(purchaseDetailId));
        if (purchaseDetail == null) return NotFound();
        var resource = PurchaseDetailResourceFromEntityAssembler.ToResourceFromEntity(purchaseDetail);
        return Ok(resource);
    }
    [HttpDelete("{purchaseDetailId:int}")]
    [SwaggerOperation(
        Summary = "Deletes a PurchaseDetail by id",
        Description = "Deletes a PurchaseDetail for a given identifier",
        OperationId = "DeletePurchaseDetailById")]
    [SwaggerResponse(200, "The PurchaseDetail was found", typeof(PurchaseDetailResource))]
    public async Task<IActionResult> DeletePurchaseDetailById(int purchaseDetailId)
    {
        var getPurchaseDetailByIdQuery = new GetPurchaseDetailByIdQuery(purchaseDetailId);
        var purchaseDetail = await purchaseDetailQueryService.Handle(getPurchaseDetailByIdQuery);
        await purchaseDetailCommandService.Delete(purchaseDetail);
        return Ok(purchaseDetail);
    }
    
    [HttpPut("{purchaseDetailId:int}")]
    [SwaggerOperation(
        Summary = "Updates a PurchaseDetail by id",
        Description = "Updates a PurchaseDetail for a given identifier",
        OperationId = "UpdatePurchaseDetailById")]
    [SwaggerResponse(200, "The PurchaseDetail was found", typeof(PurchaseDetailResource))]
    public async Task<IActionResult> UpdateCategoryById(int purchaseDetailId, [FromBody] PurchaseDetailResource purchaseDetailResource)
    {
        var getPurchaseDetailByIdQuery = new GetPurchaseDetailByIdQuery(purchaseDetailId);
        var purchaseDetail = await purchaseDetailQueryService.Handle(getPurchaseDetailByIdQuery);
        purchaseDetail.PurchaseId = purchaseDetailResource.Purchase.Id;
        purchaseDetail.ProductId = purchaseDetailResource.Product.Id;
        purchaseDetail.PurchasePrice = purchaseDetailResource.PurchasePrice;
        purchaseDetail.SalePrice = purchaseDetailResource.SalePrice;
        purchaseDetail.Quantity = purchaseDetailResource.Quantity;
        purchaseDetail.Total = purchaseDetailResource.Total;
        await purchaseDetailCommandService.Update(purchaseDetail);
        return Ok(purchaseDetail);
    }
}