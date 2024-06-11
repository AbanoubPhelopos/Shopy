using Microsoft.AspNetCore.Mvc;
using Shopy.Data;
using Shopy.Models;

namespace Shopy.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        var Categories = _context.Categories.ToList();
        return View(Categories);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Add(category);
            _context.SaveChanges();
            TempData["success"] = "The category has Created successfully";
            return RedirectToAction("Index");
        }
        return View();
    }
    
    [HttpGet]
    public IActionResult Edit(int? Id)
    {
        if (Id == null || Id == 0)
        {
            return NotFound();
        }
        var category = _context.Categories.FirstOrDefault(category => category.Id==Id);
        return View(category);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Update(category);
            _context.SaveChanges();
            TempData["Info"] = "The category has Updated successfully";
            return RedirectToAction("Index");
        }
        return View();
    }
    [HttpGet]
    public IActionResult Delete(int? Id)
    {
        if (Id == null || Id == 0)
        {
            return NotFound();
        }
        Category?  category = _context.Categories.FirstOrDefault(category => category.Id==Id);
        return View(category);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? Id)
    {
        Category? category = _context.Categories.FirstOrDefault(category => category.Id==Id);
        if (category == null)
        {
            return NotFound();
        }
        _context.Categories.Remove(category);
        _context.SaveChanges();
        TempData["error"] = "The category has deleted successfully";
        return RedirectToAction("Index");
    }
}