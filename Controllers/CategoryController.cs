using Microsoft.AspNetCore.Mvc;
using Shopy.Data;
namespace Shopy.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }
    // GET
    public IActionResult Index()
    {
        var Categories = _context.Categories.ToList();
        return View(Categories);
    }
}