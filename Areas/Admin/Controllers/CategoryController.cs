using Microsoft.AspNetCore.Mvc;
using Shopy.Entities.Models;
using Shopy.Reposatory;

namespace Shopy.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private IUnitOfWork _unitOfWork;
    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult GetTableData()
    {
        var categories = _unitOfWork.Category.GetAll();
        return Json(new { data = categories });
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
            _unitOfWork.Category.Add(category);
            _unitOfWork.Complite();
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

        var category = _unitOfWork.Category.GetFirstOrDesault(x => x.Id == Id);
        return View(category);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Complite();
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

        Category? category = _unitOfWork.Category.GetFirstOrDesault(x => x.Id == Id);
        return View(category);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? Id)
    {
        Category? category = _unitOfWork.Category.GetFirstOrDesault(x => x.Id == Id);
        if (category == null)
        {
            return NotFound();
        }
        _unitOfWork.Category.Remove(category);
        _unitOfWork.Complite();
        TempData["error"] = "The category has deleted successfully";
        return RedirectToAction("Index");
    }
}