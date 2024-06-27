using System.Net.Mime;
using Stocker_API.Sales.Domain.Model.Queries;
using Stocker_API.Sales.Domain.Services;
using Stocker_API.Sales.Interfaces.REST.Resources;
using Stocker_API.Sales.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace Stocker_API.Sales.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class SalesController(
    ISaleCommandService saleCommandService,
    ISaleQueryService saleQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleResource createSaleResource)
    {
        var createSaleCommand =
            CreateSaleCommandFromResourceAssembler.ToCommandFromResource(createSaleResource);
        var sale = await saleCommandService.Handle(createSaleCommand);
        if (sale is null) return BadRequest();
        var resource = SaleResourceFromEntityAssembler.ToResourceFromEntity(sale);
        return CreatedAtAction(nameof(GetSaleById), new { saleId = resource.Id }, resource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSales()
    {
        var getAllSalesQuery = new GetAllSalesQuery();
        var sales = await saleQueryService.Handle(getAllSalesQuery);
        var resources = sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{saleId}")]
    public async Task<IActionResult> GetSaleById([FromRoute] int saleId)
    {
        var sale = await saleQueryService.Handle(new GetSaleByIdQuery(saleId));
        if (sale == null) return NotFound();
        var resource = SaleResourceFromEntityAssembler.ToResourceFromEntity(sale);
        return Ok(resource);
    }
    
    [HttpDelete("{saleId:int}")]
    [SwaggerOperation(
        Summary = "Deletes a Sale by id",
        Description = "Deletes a Sale for a given identifier",
        OperationId = "DeleteSaleById")]
    [SwaggerResponse(200, "The Sale was found", typeof(SaleResource))]
    public async Task<IActionResult> DeleteSaleById(int saleId)
    {
        var getSaleByIdQuery = new GetSaleByIdQuery(saleId);
        var sale = await saleQueryService.Handle(getSaleByIdQuery);
        await saleCommandService.Delete(sale);
        return Ok(sale);
    }
    
    [HttpPut("{saleId:int}")]
    [SwaggerOperation(
        Summary = "Updates a category by id",
        Description = "Updates a category for a given identifier",
        OperationId = "UpdateCategoryById")]
    [SwaggerResponse(200, "The category was found", typeof(SaleResource))]
    public async Task<IActionResult> UpdateSaleById(int saleId, [FromBody] SaleResource categoryResource)
    {
        var getSaleByIdQuery = new GetSaleByIdQuery(saleId);
        var sale = await saleQueryService.Handle(getSaleByIdQuery);
        sale.ClientId = categoryResource.Client.Id;
        sale.TotalAmount = categoryResource.TotalAmount;
        await saleCommandService.Update(sale);
        return Ok(sale);
    }
    
}