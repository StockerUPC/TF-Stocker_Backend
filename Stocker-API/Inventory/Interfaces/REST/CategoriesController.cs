using System.Net.Mime;
using Stocker_API.Inventory.Domain.Model.Queries;
using Stocker_API.Inventory.Domain.Services;
using Stocker_API.Inventory.Interfaces.REST.Resources;
using Stocker_API.Inventory.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Stocker_API.Inventory.Interfaces.REST;

/**
 * Categories Controller
 * <summary>
 *     This controller is responsible for handling all the requests related to categories.
 * </summary>
 */
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class CategoriesController(
    ICategoryCommandService categoryCommandService,
    ICategoryQueryService categoryQueryService)
    : ControllerBase
{
    /**
     * Create Category.
     * <summary>
     *     This method is responsible for handling the request to create a new category.
     * </summary>
     * <param name="createCategoryResource">The resource containing the information to create a new category.</param>
     * <returns>The newly created category resource.</returns>
     */
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a category",
        Description = "Creates a category with a given name",
        OperationId = "CreateCategory")]
    [SwaggerResponse(201, "The category was created", typeof(CategoryResource))]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryResource createCategoryResource)
    {
        var createCategoryCommand =
            CreateCategoryCommandFromResourceAssembler.ToCommandFromResource(createCategoryResource);
        var category = await categoryCommandService.Handle(createCategoryCommand);
        if (category is null) return BadRequest();
        var resource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);
        return CreatedAtAction(nameof(GetCategoryById), new { categoryId = resource.Id }, resource);
    }

    /**
     * Get Category By Id.
     * <summary>
     *     This method is responsible for handling the request to get a category by its id.
     * </summary>
     * <param name="categoryId">The category identifier.</param>
     * <returns>The category resource.</returns>
     */
    [HttpGet("{categoryId:int}")]
    [SwaggerOperation(
        Summary = "Gets a category by id",
        Description = "Gets a category for a given identifier",
        OperationId = "GetCategoryById")]
    [SwaggerResponse(200, "The category was found", typeof(CategoryResource))]
    public async Task<IActionResult> GetCategoryById(int categoryId)
    {
        var getCategoryByIdQuery = new GetCategoryByIdQuery(categoryId);
        var category = await categoryQueryService.Handle(getCategoryByIdQuery);
        var resource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);
        return Ok(resource);
    }

    /**
     * Get All Categories.
     * <summary>
     *     This method is responsible for handling the request to get all categories.
     * </summary>
     * <returns>The categories resources.</returns>
     */
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all categories",
        Description = "Gets all categories",
        OperationId = "GetAllCategories")]
    [SwaggerResponse(200, "The categories were found", typeof(IEnumerable<CategoryResource>))]
    public async Task<IActionResult> GetAllCategories()
    {
        var getAllCategoriesQuery = new GetAllCategoriesQuery();
        var categories = await categoryQueryService.Handle(getAllCategoriesQuery);
        var resources = categories.Select(CategoryResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpDelete("{categoryId:int}")]
    [SwaggerOperation(
        Summary = "Deletes a category by id",
        Description = "Deletes a category for a given identifier",
        OperationId = "DeleteCategoryById")]
    [SwaggerResponse(200, "The category was found", typeof(CategoryResource))]
    public async Task<IActionResult> DeleteCategoryById(int categoryId)
    {
        var getCategoryByIdQuery = new GetCategoryByIdQuery(categoryId);
        var category = await categoryQueryService.Handle(getCategoryByIdQuery);
        await categoryCommandService.Delete(category);
        return Ok(category);
    }
    
    [HttpPut("{categoryId:int}")]
[SwaggerOperation(
        Summary = "Updates a category by id",
        Description = "Updates a category for a given identifier",
        OperationId = "UpdateCategoryById")]
    [SwaggerResponse(200, "The category was found", typeof(CategoryResource))]
    public async Task<IActionResult> UpdateCategoryById(int categoryId, [FromBody] CategoryResource categoryResource)
    {
        var getCategoryByIdQuery = new GetCategoryByIdQuery(categoryId);
        var category = await categoryQueryService.Handle(getCategoryByIdQuery);
        category.Name = categoryResource.Name;
        await categoryCommandService.Update(category);
        return Ok(category);
    }
}