using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IDataService _dataService;
    private readonly LinkGenerator _linkGenerator;

    public CategoriesController(IDataService dataService, LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }

    [HttpGet]
    public IActionResult GetCetagories(string? name = null)
    {
        IEnumerable<CategoryModel> result = null;
        if (!string.IsNullOrEmpty(name))
        {
            result = _dataService.GetCategoriesByName(name)
                .Select(CreateCategoryModel);
        }
        else
        {
            result = _dataService.GetCategories()
                .Select(CreateCategoryModel);
        }
        return Ok(result);
    }
    
    [HttpGet("{id}", Name = nameof(GetCategory))]
    public IActionResult GetCategory(int id)
    {
        var category = _dataService.GetCategory(id);
        if(category == null)
        {
            return NotFound();
        }

        return Ok(CreateCategoryModel(category));
    }

    [HttpPost]
    public IActionResult CreateCategory(CreateCategoryModel model)
    {
        var category = new Category
        {
            Name = model.Name,
            Description = model.Description
        };

        _dataService.CreateCategory(category);

        return Ok(category);
    }


    private CategoryModel CreateCategoryModel(Category category)
    {
        return new CategoryModel
        {
            //Url = $"http://localhost:5001/api/categories/{category.Id}",
            Url = GetUrl(nameof(GetCategory), new { category.Id }),
            Name = category.Name,
            Description = category.Description
        };
    }

    private string? GetUrl(string name, object values)
    {
        return _linkGenerator.GetUriByName(HttpContext, name, values);
    }
    
}
