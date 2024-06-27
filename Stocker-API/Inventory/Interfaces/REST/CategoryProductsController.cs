using System.Net.Mime;
using Stocker_API.Inventory.Domain.Model.Queries;
using Stocker_API.Inventory.Domain.Services;
using Stocker_API.Inventory.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace Stocker_API.Inventory.Interfaces.REST;

[ApiController]
[Route("/api/v1/categories/{categoryId}/products")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Categories")]
public class CategoryProductsController(IProductQueryService productQueryService) : ControllerBase
{
    /**
     * Get Tutorials by Category Id.
     * <summary>
     *     Get Tutorials for a given category.
     * </summary>
     * <param name="categoryId">Category Id</param>
     * <returns>Tutorial Resources</returns>
     */
    [HttpGet]
    public async Task<IActionResult> GetProductsByCategoryId([FromRoute] int categoryId)
    {
        var getAllProductsByCategoryIdQuery = new GetAllProductsByCategoryIdQuery(categoryId);
        var products = await productQueryService.Handle(getAllProductsByCategoryIdQuery);
        var resources = products.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}